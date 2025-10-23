using Npgsql;
using Scharff.Infrastructure.PostgreSQL.Queries.Reports.Report;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Scharff.Domain.Response.Reports.GetReportInvoiceServiceByIdTypeServicesAndDateRangeQuery;
using Scharff.Domain.Request.Report;
using Scharff.Domain.Response.Reports.GetReportServiceOrderCourier;
using System.Numerics;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Reports.GetReportServiceOrderCourier
{
    public class GetReportServiceOrderCourierQuery : IGetReportServiceOrderCourierQuery
    {
        private readonly IDbConnection _connection;

        public GetReportServiceOrderCourierQuery(IDbConnection connection)
        { 
            _connection = connection;
        }
        public async Task<DataSet> GetReportServiceOrderCourier(RequestReportServiceOrderCourier request)
        {
            using IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString);

            try
            { 
                string query = @"
                    WITH detalle_parametro_val AS (
                        SELECT id 
                        FROM nsf.detalle_parametro 
                        WHERE codigo_detalle='VAL' AND codigo_general='ESTINVOICE'
                    ),
                    orden_facturable_servicio_agg AS (
                        SELECT 
                          ofact.id,
                          ofact.numero_orden,
                          ROUND(SUM(ofaSe.monto_venta), 2) AS Unit_Amount,
                          ROUND(SUM(CASE WHEN eos.id_servicio <> (SELECT id FROM nsf.servicio WHERE codigo_nsf='STI') THEN ofaSe.monto_venta ELSE 0 END), 2) AS Unit_Amount_sin_fuel,
                          ROUND(SUM(CASE WHEN eos.id_servicio = (SELECT id FROM nsf.servicio WHERE codigo_nsf='STI') THEN ofaSe.monto_venta ELSE 0 END), 2) AS Unit_Amount_fuel,
                          ROUND(SUM(ofaSe.monto_descuento), 2) AS discount_amount,
                          ROUND(SUM(ofaSe.monto_total), 2) AS subtotal_amount,
                          ROUND(SUM(ofaSe.monto_total * (impu.tasa / 100)), 2) AS igv_amount,
                          ROUND(SUM(ofaSe.monto_total + (ofaSe.monto_total * (impu.tasa / 100))), 2) AS sales_price_amount,
                          MAX(H.id_agrupacion) AS categorias,
                          MAX(COALESCE(impu.tasa, 0) / 100) AS tasa,
                          ROUND(SUM(CASE WHEN impu.tasa > 0 THEN ofaSe.monto_total ELSE 0 END), 2) AS affect_amount,
                          COALESCE(
                            (SELECT SUM(COALESCE(ifga.importe_no_pago, 0)) 
                             FROM nsf.invoice_fedex_guia ax 
                             LEFT JOIN nsf.invoice_fedex_guia_acreditacion ifga ON ax.id = ifga.id_invoice_fedex_guia 
                             WHERE CAST(ax.item AS varchar) = ofact.numero_orden 
                               AND ax.id_categoria IN (MAX(H.id_agrupacion))
                               AND ax.id_estado = (SELECT id FROM detalle_parametro_val)
                               AND ifga.estado = true), 0.00
                          ) AS accreditation_amount
                        FROM
                          nsf.orden_facturable_servicio ofaSe 
                          INNER JOIN nsf.estructura_organizacional_servicio eos on (ofaSe.id_servicio= eos.id) 
                          LEFT JOIN nsf.orden_facturable ofact ON ofact.id = ofaSe.id_orden_facturable
	                      LEFT JOIN nsf.servicio serv  on (serv.id = eos.id_servicio ) 
                          LEFT JOIN nsf.detalle_parametro dpcse ON serv.id_categoria = dpcse.id AND dpcse.codigo_general = 'CATSERV'
                          LEFT JOIN nsf.impuesto impu ON serv.id_impuesto = impu.id
                          LEFT JOIN nsf.invoice_fedex_homologacion H ON CAST(H.codigo_nsf AS integer) = eos.id_servicio
                                                                       AND H.id_tipo = (SELECT id FROM nsf.detalle_parametro WHERE codigo_general = 'TIPOHOINVOICE' AND codigo_detalle = 'SERV')
                        WHERE
                          serv.estado = true 
                          AND dpcse.estado = true 
                          AND impu.estado = true 
                          AND ofact.id = ANY(@GuideList) 
                          AND dpcse.id = @service_category_id
                        GROUP BY ofact.id, ofact.numero_orden
                    )



                    SELECT 
                        LPAD(CAST(C.id AS TEXT), 8, '0') AS client_id,
                        C.descripcion_negocio AS company_name,
                        (UPPER(dpTdoc.descripcion_corta) || ': ' || C.numero_documento_identidad) AS identity_document_number,
                        C.telefono AS phone,
                        '' AS Fax,
                        EMP.razon_social AS Company,
                        coalesce(dirCli.direccion, 'Sin Dirección') AS client_address,
                        A.id AS billable_order_id,
	                    ROUND(ofase.discount_amount, 2) AS usd_descuento, -- Aquí se añade el campo usd_descuento
                        ROUND(((affect_amount - (affect_amount * (accreditation_amount / subtotal_amount)))) * ofase.tasa, 2) AS igv_descuento_global,
                        ROW_NUMBER() OVER (ORDER BY g.fecha_guia DESC) AS item,
                        A.numero_referencia AS no_awb, -- Aquí se añade el campo no_awb
                        g.fecha_guia AS guide_date,
                        g.descripcion_paquete AS package_description,
                        g.remitente_contacto AS sender_contact,
                        g.codigo_ciudad_destino AS destination_city_code,
                        tip_emb.descripcion AS packaging_type,	
                        g.cantidad_paquete AS package_quantity,
                        ROUND(g.peso_paquete, 2) AS package_weight,
                        ROUND(g.peso_volumen, 2) AS package_weight_volumetric,
                        ofase.Unit_Amount_sin_fuel AS usd_tarifa,	 
	                    CAST(ROUND(ofase.discount_amount / ofase.Unit_Amount_sin_fuel * 100, 2) AS VARCHAR) || '%' AS discount_percentage,
   	                    ROUND(ofase.discount_amount, 2) AS discount_amount,
   	                    ROUND(ofase.Unit_Amount_sin_fuel - ofase.discount_amount, 2) AS subtotal_discount_amount,
	                    ofase.Unit_Amount_fuel AS usd_fuel,
	                    ofase.accreditation_amount AS acreditacion,
	                    ofase.subtotal_amount - ofase.accreditation_amount AS usd_neto    
    
                    FROM
                        nsf.orden_facturable A
                        INNER JOIN nsf.estructura_organizacional_base D ON D.id = A.id_estructura_organizacional_base
                        LEFT JOIN nsf.empresa EMP ON D.id_empresa = EMP.id
                        INNER JOIN orden_facturable_servicio_agg ofase ON ofase.id = A.id
                        LEFT JOIN nsc.guia g ON A.numero_orden = CAST(g.id AS varchar) 
                                              AND A.id_tipo_orden = (SELECT id FROM nsf.detalle_parametro WHERE codigo_detalle='DOF' AND codigo_general='TIPDOCFAC')
                        LEFT JOIN nsf.cliente cgui ON cgui.id = g.remitente_id
                        LEFT JOIN nsf.cliente C ON 1=1
                        LEFT JOIN nsc.detalle_entidad_courier tip_emb ON g.id_embalaje = tip_emb.id
                        LEFT JOIN nsf.detalle_parametro dpTdoc ON C.id_tipo_documento = dpTdoc.id AND dpTdoc.codigo_general = '10'
                        LEFT JOIN (
                            SELECT dcli.id_cliente, dcli.direccion
                            FROM nsf.direccion_cliente dcli
                            LEFT JOIN nsf.detalle_parametro dp ON dcli.id_tipo_direccion = dp.id AND dp.codigo_general = '9'
                            LEFT JOIN nsf.detalle_parametro dp2 ON dcli.estado = dp2.id AND dp2.codigo_general = 'ESTCONT'
                            WHERE dp.codigo_detalle = '1' AND dp2.codigo_detalle = 'ESTCONTACT'
                        ) dirCli ON dirCli.id_cliente = C.id
                    WHERE
                        A.id_estado <> (SELECT id FROM nsf.detalle_parametro WHERE codigo_detalle='ACRE' AND codigo_general='ESTDOF')
                        AND A.id = ANY(@GuideList) 
	                    AND (ofase.categorias = @category_id_invoice OR @category_id_invoice  = 0)
                        AND D.id_empresa = @company_id
                        AND C.id = @client_id
                    ORDER BY
	                    item asc,
                        A.fecha_creacion DESC;



                ";
                var guideIds = request.guide_list.Select(g => g.billable_order_id).ToArray();

                var arg = new { GuideList = guideIds, request.service_category_id, request.category_id_invoice, request.company_id, request.client_id };

                var result = await connection.QueryAsync<ResponseReportServiceOrderCourier>(query, arg);

                if (result.Any())
                {
                    // Crear DataSet
                    DataSet dataSet = new DataSet();

                    // Crear DataTable para la cabecera
                    DataTable headerTable = new DataTable("Header");
                    
                    headerTable.Columns.Add("client_id", typeof(string));
                    headerTable.Columns.Add("company_name", typeof(string));
                    headerTable.Columns.Add("identity_document_number", typeof(string));
                    headerTable.Columns.Add("phone", typeof(string));
                    headerTable.Columns.Add("Fax", typeof(string));
                    headerTable.Columns.Add("company", typeof(string));
                    headerTable.Columns.Add("client_address", typeof(string));
                    headerTable.Columns.Add("billable_order_id", typeof(string));
                    headerTable.Columns.Add("usd_descuento", typeof(string));
                    headerTable.Columns.Add("igv_descuento_global", typeof(string));
                    headerTable.Columns.Add("item", typeof(string));
                    headerTable.Columns.Add("no_awb", typeof(string));
                    headerTable.Columns.Add("guide_date", typeof(string));
                    headerTable.Columns.Add("package_description", typeof(string));
                    headerTable.Columns.Add("sender_contact", typeof(string));
                    headerTable.Columns.Add("destination_city_code", typeof(string));
                    headerTable.Columns.Add("packaging_type", typeof(string));
                    headerTable.Columns.Add("package_quantity", typeof(string));
                    headerTable.Columns.Add("package_weight", typeof(string));
                    headerTable.Columns.Add("package_weight_volumetric", typeof(string));                    
                    headerTable.Columns.Add("usd_tarifa", typeof(string));
                    headerTable.Columns.Add("discount_percentage", typeof(string));
					headerTable.Columns.Add("discount_amount", typeof(string));
                    headerTable.Columns.Add("subtotal_discount_amount", typeof(string));
                    headerTable.Columns.Add("usd_fuel", typeof(string));
                    headerTable.Columns.Add("acreditacion", typeof(string));
                    headerTable.Columns.Add("usd_neto", typeof(string));



                    DataRow headerRow = headerTable.NewRow();

                    headerRow["client_id"] = "client_id";
                    headerRow["company_name"] = "company_name";
                    headerRow["identity_document_number"] = "identity_document_number";
                    headerRow["phone"] = "phone";
                    headerRow["Fax"] = "Fax";
                    headerRow["company"] = "empresa";
                    headerRow["client_address"] = "Direccion_cliente";
                    headerRow["billable_order_id"] = "idordenFacturable";
                    headerRow["usd_descuento"] = "usd_descuento";
                    headerRow["igv_descuento_global"] = "igv_descuento_global";
                    headerRow["item"] = "Itm";
                    headerRow["no_awb"] = "No AWB";
                    headerRow["guide_date"] = "Fecha";
					headerRow["package_description"] = "Descripcion";
					headerRow["sender_contact"] = "Contacto";
					headerRow["destination_city_code"] = "Dst";
                    headerRow["packaging_type"] = "Tip Emb";
                    headerRow["package_quantity"] = "Pz";
					headerRow["package_weight"] = "Peso";
					headerRow["package_weight_volumetric"] = "Peso Vol.";
					headerRow["usd_tarifa"] = "USD TARIFA";
					headerRow["discount_percentage"] = "% Dscto";
					headerRow["discount_amount"] = "USD Dscto";
                    headerRow["subtotal_discount_amount"] = "USD Imp Dscto";
                    headerRow["usd_fuel"] = "USD FUEL";
					headerRow["acreditacion"] = "Acredit."; 
					headerRow["usd_neto"] = "USD NETO";
					headerTable.Rows.Add(headerRow);
 

                    // Agregar DataTable de cabecera al DataSet
                    dataSet.Tables.Add(headerTable);

                    // Crear un nuevo DataTable
                    DataTable contentTable = new DataTable();

                    contentTable.Columns.Add("client_id", typeof(string));
                    contentTable.Columns.Add("company_name", typeof(string));
                    contentTable.Columns.Add("identity_document_number", typeof(string));
                    contentTable.Columns.Add("phone", typeof(string));
                    contentTable.Columns.Add("Fax", typeof(string));
                    contentTable.Columns.Add("company", typeof(string));
                    contentTable.Columns.Add("client_address", typeof(string));                
                    contentTable.Columns.Add("billable_order_id", typeof(string));
					contentTable.Columns.Add("usd_descuento", typeof(string));
					contentTable.Columns.Add("igv_descuento_global", typeof(string));
					contentTable.Columns.Add("item", typeof(int));
					contentTable.Columns.Add("no_awb", typeof(string));
					contentTable.Columns.Add("guide_date", typeof(string));
					contentTable.Columns.Add("package_description", typeof(string));						
					contentTable.Columns.Add("sender_contact", typeof(string));						
					contentTable.Columns.Add("destination_city_code", typeof(string));
					contentTable.Columns.Add("packaging_type", typeof(string));	
					contentTable.Columns.Add("package_quantity", typeof(string));						
					contentTable.Columns.Add("package_weight", typeof(string));
					contentTable.Columns.Add("package_weight_volumetric", typeof(string));
					contentTable.Columns.Add("usd_tarifa", typeof(string));
					contentTable.Columns.Add("discount_percentage", typeof(string));
					contentTable.Columns.Add("discount_amount", typeof(string));
                    contentTable.Columns.Add("subtotal_discount_amount", typeof(string));
                    contentTable.Columns.Add("usd_fuel", typeof(string));
					contentTable.Columns.Add("acreditacion", typeof(string)); 
					contentTable.Columns.Add("usd_neto", typeof(string)); 
					// Agregar cada objeto de ResponseReportServiceOrderCourier como una fila en el DataTable
					foreach (var obj in result)

					{
                        contentTable.Rows.Add(
                        obj.client_id,
                        obj.company_name,
                        obj.identity_document_number,
                        obj.phone,
                        obj.Fax,
                        obj.company,
                        obj.client_address,
                        obj.billable_order_id,
                        obj.usd_descuento,
                        obj.igv_descuento_global,
                        obj.item,
                        obj.no_awb,
                        obj.guide_date,
						obj.package_description,
						obj.sender_contact,
						obj.destination_city_code,
                        obj.packaging_type,
                        obj.package_quantity,
						obj.package_weight,
						obj.package_weight_volumetric,
						obj.usd_tarifa,
						obj.discount_percentage,
						obj.discount_amount,
                        obj.subtotal_discount_amount,
                        obj.usd_fuel,
						obj.acreditacion, 
						obj.usd_neto);

                    }	 
                     
                    // Agregar DataTable de contenido al DataSet
                    dataSet.Tables.Add(contentTable);

                    // Devolver el DataSet
                    return dataSet;
                }
                // Si no hay resultados, devolver un DataSet vacío
                return new DataSet();
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                throw;
            }
        }
    }
}
