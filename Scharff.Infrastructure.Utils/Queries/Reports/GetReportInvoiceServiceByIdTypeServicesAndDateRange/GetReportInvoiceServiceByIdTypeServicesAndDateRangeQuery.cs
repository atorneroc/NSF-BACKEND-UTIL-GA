using Dapper;
using Npgsql;
using Scharff.Domain.Response.Reports.GetReportInvoiceServiceByIdTypeServicesAndDateRangeQuery;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
namespace Scharff.Infrastructure.Http.Queries.Reports.GetReportInvoiceServiceByIdTypeServicesAndDateRange
{
    public class GetReportInvoiceServiceByIdTypeServicesAndDateRangeQuery : IGetReportInvoiceServiceByIdTypeServicesAndDateRange
    {
        private readonly IDbConnection _connection;

        public GetReportInvoiceServiceByIdTypeServicesAndDateRangeQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<DataSet> GetReportAcreditations(DateTime issue_Date_Start, DateTime issue_Date_End)
        {
            using IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString);

            try
            {
                string query = @" 
                                 select * 
                                       from ( 
                                               select  
                                                   ROW_NUMBER() OVER (ORDER BY fecha_acreditacion desc, ifga.item desc) as item
                                                   --ifg.id,
                                                   --ifga.item as item 
                                                   ,dpa2.descripcion as concept
                                                   ,TO_CHAR(fecha_acreditacion, 'DD/MM/YYYY') AS date_acreditations
                                                   ,dpa.descripcion as code_acreditations
                                                   ,numero_caso as case_number
                                                   ,ifx.numero_invoice as invoice_number
                                                   ,ifg.numero_guia as guide_number
                                                   ,CAST(cli.id AS varchar ) as  code_client
                                                   ,cli.descripcion_negocio  as client
                                                   ,pro.descripcion as product
                                                   ,COALESCE(ifgs1.monto_servicio,0) as freight
                                                   ,COALESCE(ifgs2.monto_servicio,0) as fuel
                                                   ,COALESCE(ifgs3.monto_servicio,0) as other
                                                   ,(COALESCE(ifgs1.monto_descuento,0) + COALESCE(ifgs2.monto_descuento,0) + COALESCE(ifgs3.monto_descuento,0))  discount
                                                   ,COALESCE(ifg.monto_total,0) as total
                                                   ,COALESCE(ifga.importe_no_pago,0) as no_payment
                                                   ,(COALESCE(ifg.monto_total,0) - COALESCE(ifga.importe_no_pago,0)) as invoice_difference
                                                   ,COALESCE(usu.nombre_completo,'') as recorder
                                                   ,ifga.observacion  as observations

                                               from
                                                   nsf.invoice_fedex_guia_acreditacion ifga
                                                   left join nsf.invoice_fedex_guia ifg on (ifga.id_invoice_fedex_guia = ifg.id )
                                                   left join nsf.invoice_fedex 	 ifx on (ifx.id = ifg.id_invoice_fedex )
                                                   left join nsf.orden_facturable ofa on ( CAST(ifg.item  AS varchar) = ofa.numero_orden )
                                                   left join nsf.detalle_parametro dpa on dpa.id = ifga.id_motivo and  dpa.codigo_general = 'MOTIVOACREDITACION'
                                                   left join nsf.cliente cli on ofa.id_cliente = cli.id
                                                   left join nsf.producto pro on pro.id_producto = ifg.id_producto
                                                   left join nsf.invoice_fedex_guia_servicio ifgs1 on ifgs1.id_invoice_fedex_guia =ifg.id  and ifgs1.id_servicio =(select CAST(codigo_nsf AS INT)  from nsf.invoice_fedex_homologacion where descripcion_fedex='FLETE')
                                                   left join nsf.invoice_fedex_guia_servicio ifgs2 on ifgs2.id_invoice_fedex_guia =ifg.id  and ifgs2.id_servicio =(select CAST(codigo_nsf AS INT) from nsf.invoice_fedex_homologacion where descripcion_fedex='SOBRECOSTO')
                                                   left join nsf.invoice_fedex_guia_servicio ifgs3 on ifgs3.id_invoice_fedex_guia =ifg.id  and ifgs3.id_servicio =(select CAST(codigo_nsf AS INT) from nsf.invoice_fedex_homologacion where descripcion_fedex='DERECHOS')
                                                   left join nsf.usuario_perfil upe on (upe.id_usuario = ifga.id_registrador and upe.id_perfil =  (select id from   nsf.perfil where codigo_oficial='002' and estado = true))
                                                   left join nsf.usuario usu on ( usu.id =  upe.id_usuario ) 	   
                                                   left join nsf.detalle_parametro dpa2 on dpa2.id = ifg.id_categoria and  dpa2.codigo_general = 'AGRUPACIONSERVICIO' --categoria de la guia
                                               where 
                                                       ifga.estado= true 
	   			                                       and ifg.id_estado in (select id from nsf.detalle_parametro where codigo_general='ESTINVOICE' and codigo_detalle in ('VAL','ACRE'))
                                                       and DATE(ifga.fecha_acreditacion) BETWEEN DATE(@issue_Date_Start) AND DATE(@issue_Date_End)  
                                               ) tmp
                ";
                var arg = new { issue_Date_Start, issue_Date_End };

                var result = await connection.QueryAsync<ResponseReportInvoiceServiceAcreditation>(query, arg);

                if (result.Any())
                {
                    // Crear DataSet
                    DataSet dataSet = new DataSet();

                    // Crear DataTable para la cabecera
                    DataTable headerTable = new DataTable("Header");
                    headerTable.Columns.Add("item", typeof(string));
                    headerTable.Columns.Add("concept", typeof(string));
                    headerTable.Columns.Add("date_acreditations", typeof(string));
                    headerTable.Columns.Add("code_acreditations", typeof(string));
                    headerTable.Columns.Add("case_number", typeof(string));
                    headerTable.Columns.Add("invoice_number", typeof(string));
                    headerTable.Columns.Add("guide_number", typeof(string));
                    headerTable.Columns.Add("code_client", typeof(string));
                    headerTable.Columns.Add("client", typeof(string));
                    headerTable.Columns.Add("product", typeof(string));
                    headerTable.Columns.Add("freight", typeof(string));
                    headerTable.Columns.Add("fuel", typeof(string));
                    headerTable.Columns.Add("other", typeof(string));
                    headerTable.Columns.Add("discount", typeof(string));
                    headerTable.Columns.Add("total", typeof(string));
                    headerTable.Columns.Add("no_payment", typeof(string));
                    headerTable.Columns.Add("invoice_difference", typeof(string));
                    headerTable.Columns.Add("recorder", typeof(string));
                    headerTable.Columns.Add("observations", typeof(string));
                    // Añade más columnas de cabecera según sea necesario

                    // Agregar fila de cabecera
                    DataRow headerRow = headerTable.NewRow();
                    headerRow["item"] = "N°";
                    headerRow["concept"] = "CONCEPTO";
                    headerRow["date_acreditations"] = "FECHA ACREDIT.";
                    headerRow["code_acreditations"] = "COD ACREDIT.";
                    headerRow["case_number"] = "N° DE CASO";
                    headerRow["invoice_number"] = "# INVOICE";
                    headerRow["guide_number"] = "# GUIA";
                    headerRow["code_client"] = "COD. CLIENTE";
                    headerRow["client"] = "CLIENTE";
                    headerRow["product"] = "PROD.";
                    headerRow["freight"] = "FLETE";
                    headerRow["fuel"] = "FUEL";
                    headerRow["other"] = "OTHER";
                    headerRow["discount"] = "DSCTO";
                    headerRow["total"] = "TOTAL";
                    headerRow["no_payment"] = "NO PAGO";
                    headerRow["invoice_difference"] = "DIF. INVOICE";
                    headerRow["recorder"] = "REGISTRADO POR";
                    headerRow["observations"] = "OBSERVACIONES";
                    // Añade más valores de cabecera según sea necesario
                    headerTable.Rows.Add(headerRow);

                    // Agregar DataTable de cabecera al DataSet
                    dataSet.Tables.Add(headerTable);

                    // Crear un nuevo DataTable
                    DataTable contentTable = new DataTable();
                    contentTable.Columns.Add("item", typeof(string));
                    contentTable.Columns.Add("concept", typeof(string));
                    contentTable.Columns.Add("date_acreditations", typeof(string));
                    contentTable.Columns.Add("code_acreditations", typeof(string));
                    contentTable.Columns.Add("case_number", typeof(string));
                    contentTable.Columns.Add("invoice_number", typeof(string));
                    contentTable.Columns.Add("guide_number", typeof(string));
                    contentTable.Columns.Add("code_client", typeof(string));
                    contentTable.Columns.Add("client", typeof(string));
                    contentTable.Columns.Add("product", typeof(string));
                    contentTable.Columns.Add("freight", typeof(decimal));
                    contentTable.Columns.Add("fuel", typeof(decimal));
                    contentTable.Columns.Add("other", typeof(decimal));
                    contentTable.Columns.Add("discount", typeof(decimal));
                    contentTable.Columns.Add("total", typeof(decimal));
                    contentTable.Columns.Add("no_payment", typeof(decimal));
                    contentTable.Columns.Add("invoice_difference", typeof(decimal));
                    contentTable.Columns.Add("recorder", typeof(string));
                    contentTable.Columns.Add("observations", typeof(string));

                    // Agregar cada objeto de ResponseInvoiceFedexHomologacion como una fila en el DataTable
                    foreach (var obj in result)
                    {
                        contentTable.Rows.Add(
                            obj.item,
                            obj.concept,
                            obj.date_acreditations,
                            obj.code_acreditations,
                            obj.case_number,
                            obj.invoice_number,
                            obj.guide_number,
                            obj.code_client,
                            obj.client,
                            obj.product,
                            obj.freight,
                            obj.fuel,
                            obj.other,
                            obj.discount,
                            obj.total,
                            obj.no_payment,
                            obj.invoice_difference,
                            obj.recorder,
                            obj.observations);

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

        public async Task<DataSet> GetReportfreight(DateTime issue_Date_Start, DateTime issue_Date_End)
        {
            using IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString);

            try
            {

                //string query = "SELECT * FROM table WHERE idTypeService = @idTypeService AND issue_Date BETWEEN @issue_Date_Start AND @issue_Date_End";
                string query = @"
                    select 
					item,
					date,
					invoice_number,
					guide_number,
					code_client,
					client,
					product,
					freight,
					fuel,
					discount,
					total_invoice,
					total_acreditation,
					total_fedex,
					type_document,
					serie,
					numero,
					fecha_emision,				   
                    moneda,
				    valor_tipo_cambio,
                    amount_payment, 
					amount,
					total_fedex - acumulado  as difference,
                    description_status_guide,
                    guide_observation
					From 
					(
					
						select 	
							   ROW_NUMBER() OVER (ORDER BY ife.fecha_emision,ifg.numero_guia, icom.id ) as item 
							  ,TO_CHAR(ife.fecha_emision, 'DD/MM/YYYY') AS date
							  ,ife.numero_invoice as invoice_number
							  ,ifg.numero_guia as guide_number 
							  ,COALESCE(CAST(cli.id AS varchar),'') as  code_client
							  ,COALESCE(cli.descripcion_negocio,'')  as client
							  ,pro.descripcion as product
							  ,COALESCE(ifgs1.monto_servicio,0) as freight
							  ,COALESCE(ifgs2.monto_servicio,0) as fuel
							  ,COALESCE(ifgs1.monto_descuento,0) as discount  --monto descuento flete
							  ,COALESCE(ifg.monto_total,0) as total_invoice
							  ,COALESCE(ifga.total_acreditaciones,0) as total_acreditation
							  , COALESCE(ifg.monto_total,0) - COALESCE(ifga.total_acreditaciones,0)   AS 	total_fedex
							  , COALESCE(icom.codigo_detalle_tcp,'') as	type_document
							  , COALESCE(icom.serie,'') as serie
							  , COALESCE(icom.correlativo,'')  as numero
							  , COALESCE(TO_CHAR(icom.fecha_emision , 'DD/MM/YYYY'),'') as fecha_emision
							  , COALESCE(icom.codigo_moneda,'') as moneda 
							  , COALESCE(icom.valor_tipo_cambio,0) as valor_tipo_cambio
                              , COALESCE(icom.monto_comprobante,0) as amount_payment   
							  , COALESCE(icom.monto_venta,0) as amount 
							  , COALESCE(icom.acumulado,0) as acumulado
					          , coalesce(dps5.descripcion,'') as description_status_guide
					          , coalesce(ifg.observacion,'') as guide_observation  
									  from 
										  nsf.invoice_fedex ife 
										  left join nsf.invoice_fedex_guia ifg on (ifg.id_invoice_fedex = ife.id )		
										  left join nsf.orden_facturable ofa on ( CAST(ifg.item  AS varchar) = ofa.numero_orden )
										  left join nsf.cliente cli on ofa.id_cliente = cli.id
										  left join nsf.producto pro on pro.id_producto = ifg.id_producto
										  left join nsf.invoice_fedex_guia_servicio ifgs1 on ifgs1.id_invoice_fedex_guia =ifg.id  and ifgs1.id_servicio =(select CAST(codigo_nsf AS INT)  from nsf.invoice_fedex_homologacion where descripcion_fedex='FLETE')
										  left join nsf.invoice_fedex_guia_servicio ifgs2 on ifgs2.id_invoice_fedex_guia =ifg.id  and ifgs2.id_servicio =(select CAST(codigo_nsf AS INT) from nsf.invoice_fedex_homologacion where descripcion_fedex='SOBRECOSTO')
										  left join nsf.invoice_fedex_guia_servicio ifgs3 on ifgs3.id_invoice_fedex_guia =ifg.id  and ifgs3.id_servicio =(select CAST(codigo_nsf AS INT) from nsf.invoice_fedex_homologacion where descripcion_fedex='DERECHOS')
										  left join nsf.servicio 			  	serv1	on  (serv1.id = ifgs1.id_servicio) 
										  left join nsf.importe_comprobantes()  icom on (ofa.id = icom.id_dof and serv1.id_categoria = icom.id_categoria)
										  left join nsf.detalle_parametro 		dps1 ON (ifgs1.id_estado = dps1.id AND dps1.codigo_general = 'ESTINVOICE')
										  left join nsf.detalle_parametro 		dps2 ON (ifgs2.id_estado = dps2.id AND dps2.codigo_general = 'ESTINVOICE')
        								  left join nsf.detalle_parametro 		dps5 ON (ifg.id_estado 	 = dps5.id AND dps5.codigo_general = 'ESTINVOICE')
										  left join nsf.detalle_parametro 		dps4  on (ifgs1.id_estado = ofa.id_tipo_orden AND dps4.codigo_general = 'TIPDOCFAC' and dps4.codigo_detalle ='DOF')

										  left join (select 
														   id_invoice_fedex_guia,sum(COALESCE(importe_no_pago,0)) as total_acreditaciones 
													  from nsf.invoice_fedex_guia_acreditacion where estado = true
													  Group by id_invoice_fedex_guia) ifga on ifga.id_invoice_fedex_guia = ifg.id

										  -------------------------------------------------------------------------------------
										  where   
											   dps1.codigo_detalle in ('PRO','VAL','VALOBS','VALPARC','ERROR','ACRE' ) -- Estado Servicio FLETE 
											   and dps2.codigo_detalle in ('PRO','VAL','VALOBS','VALPARC','ERROR','ACRE' ) -- Estado Servicio SOBRECOSTO 
											   and ifg.id_estado in (select id from nsf.detalle_parametro where codigo_general='ESTINVOICE' and codigo_detalle in ('PRO','VAL','VALOBS','VALPARC','ERROR','ACRE' )) 
											   and  DATE(ife.fecha_emision) BETWEEN DATE(@issue_Date_Start) AND DATE(@issue_Date_End)  
											   --and  DATE(ife.fecha_emision) BETWEEN '2024-01-01' and '2024-05-28' 
											   order by  ife.fecha_emision  ,ifg.numero_guia, icom.id
							) tb_informe_freight	
                ";

                var arg = new { issue_Date_Start, issue_Date_End };
                var result = await connection.QueryAsync<ResponseReportInvoiceServiceFreight>(query, arg);
                if (result.Any())
                {
                    // Crear DataSet
                    DataSet dataSet = new DataSet();

                    // Crear DataTable para la cabecera
                    DataTable headerTable = new DataTable("Header");
                    headerTable.Columns.Add("item", typeof(string));
                    headerTable.Columns.Add("date", typeof(string));
                    headerTable.Columns.Add("invoice_number", typeof(string));
                    headerTable.Columns.Add("guide_number", typeof(string));
                    headerTable.Columns.Add("description_status_guide", typeof(string));
                    headerTable.Columns.Add("code_client", typeof(string));
                    headerTable.Columns.Add("client", typeof(string));
                    headerTable.Columns.Add("product", typeof(string));
                    headerTable.Columns.Add("freight", typeof(string));
                    headerTable.Columns.Add("fuel", typeof(string));
                    headerTable.Columns.Add("discount", typeof(string));
                    headerTable.Columns.Add("total_invoice", typeof(string));
                    headerTable.Columns.Add("total_acreditation", typeof(string));
                    headerTable.Columns.Add("total_fedex", typeof(string));
                    headerTable.Columns.Add("type_document", typeof(string));
                    headerTable.Columns.Add("serie", typeof(string));
                    headerTable.Columns.Add("numero", typeof(string));
                    headerTable.Columns.Add("fecha_emision", typeof(string)); 
                    headerTable.Columns.Add("moneda", typeof(string));
                    headerTable.Columns.Add("valor_tipo_cambio", typeof(string));
                    headerTable.Columns.Add("amount_payment", typeof(string));
                    headerTable.Columns.Add("amount", typeof(string));
                    headerTable.Columns.Add("difference", typeof(string));
                    headerTable.Columns.Add("guide_observation", typeof(string));
                    // Añade más columnas de cabecera según sea necesario

                    // Agregar fila de cabecera
                    DataRow headerRow = headerTable.NewRow();
                    headerRow["item"] = "N°";
                    headerRow["date"] = "FECHA";
                    headerRow["invoice_number"] = "# INVOICE";
                    headerRow["guide_number"] = "# GUIA";
                    headerRow["description_status_guide"] = "ESTADO";
                    headerRow["code_client"] = "COD. CLIENTE";
                    headerRow["client"] = "CLIENTE";
                    headerRow["product"] = "PROD.";
                    headerRow["freight"] = "FLETE";
                    headerRow["fuel"] = "FUEL";
                    headerRow["discount"] = "DSCTO";
                    headerRow["total_invoice"] = "TOTAL INVOICE";
                    headerRow["total_acreditation"] = "TOTAL ACREDIT.";
                    headerRow["total_fedex"] = "TOTAL FEDEX";
                    headerRow["type_document"] = "T. DOC";
                    headerRow["serie"] = "SERIE";
                    headerRow["numero"] = "NUMERO";
                    headerRow["fecha_emision"] = "FCH. EMI";
                    headerRow["moneda"] = "MONEDA";
                    headerRow["valor_tipo_cambio"] = "T/C";
                    headerRow["amount_payment"] = "IMPORTE COMPROBANTE";
                    headerRow["amount"] = "IMPORTE";
                    headerRow["difference"] = "DIFERENCIA";
                    headerRow["guide_observation"] = "OBSERVACIONES";
                    // Añade más valores de cabecera según sea necesario
                    headerTable.Rows.Add(headerRow);

                    // Agregar DataTable de cabecera al DataSet
                    dataSet.Tables.Add(headerTable);

                    // Crear un nuevo DataTable
                    DataTable contentTable = new DataTable();
                    contentTable.Columns.Add("item", typeof(int));
                    contentTable.Columns.Add("date", typeof(string));
                    contentTable.Columns.Add("invoice_number", typeof(string));
                    contentTable.Columns.Add("guide_number", typeof(string));
                    contentTable.Columns.Add("description_status_guide", typeof(string));
                    contentTable.Columns.Add("code_client", typeof(string));
                    contentTable.Columns.Add("client", typeof(string));
                    contentTable.Columns.Add("product", typeof(string));
                    contentTable.Columns.Add("freight", typeof(decimal));
                    contentTable.Columns.Add("fuel", typeof(decimal));
                    contentTable.Columns.Add("discount", typeof(decimal));
                    contentTable.Columns.Add("total_invoice", typeof(decimal));
                    contentTable.Columns.Add("total_acreditation", typeof(decimal));
                    contentTable.Columns.Add("total_fedex", typeof(decimal));
                    contentTable.Columns.Add("type_document", typeof(string));
                    contentTable.Columns.Add("serie", typeof(string));
                    contentTable.Columns.Add("numero", typeof(string));
                    contentTable.Columns.Add("fecha_emision", typeof(string));
                    contentTable.Columns.Add("moneda", typeof(string));
                    contentTable.Columns.Add("valor_tipo_cambio", typeof(decimal));
                    contentTable.Columns.Add("amount_payment", typeof(decimal));
                    contentTable.Columns.Add("amount", typeof(decimal));
                    contentTable.Columns.Add("difference", typeof(decimal));
                    contentTable.Columns.Add("guide_observation", typeof(string));

                    // Agregar cada objeto de ResponseInvoiceFedexHomologacion como una fila en el DataTable
                    foreach (var obj in result)
                    {
                        //dataTable.Rows.Add(obj.origin_description, obj.desc_type, obj.destination_id_nsf, obj.destination_id_nsc, obj.group_id, obj.born_from_the_invoice);
                        contentTable.Rows.Add(obj.item,
                                           obj.date,
                                            obj.invoice_number,
                                            obj.guide_number,
                                            obj.description_status_guide,
                                            obj.code_client,
                                            obj.client,
                                            obj.product,
                                            obj.freight,
                                            obj.fuel,
                                            obj.discount,
                                            obj.total_invoice,
                                            obj.total_acreditation,
                                            obj.total_fedex,
                                            obj.type_document,
                                            obj.serie,
                                            obj.numero,
                                            obj.fecha_emision,
                                            obj.moneda,
                                            obj.valor_tipo_cambio,
                                            obj.amount_payment,
                                            obj.amount,
                                            obj.difference,
                                            obj.guide_observation
                                            );
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

        public async Task<DataSet> GetReportFee(DateTime issue_Date_Start, DateTime issue_Date_End)
        {
            using IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString);

            try
            {

                //string query = "SELECT * FROM table WHERE idTypeService = @idTypeService AND issue_Date BETWEEN @issue_Date_Start AND @issue_Date_End";
                string query = @"
                   select
	               item,
			       date,
			       invoice_number,
			       guide_number,
			       code_client,
			       client,
			       product,
			       derechos_exterior,
			       discount,
			       total_invoice,
			       total_acreditation,
			       total_fedex,
			       type_document,
			       serie,
			       numero,
			       fecha_emision,
				   moneda,
				   valor_tipo_cambio,
                   amount_payment, 
			       amount,
			       total_fedex - acumulado  as difference,
                   description_status_guide,
                   guide_observation
	               from
		            (                
			             select 	
				                ROW_NUMBER() OVER (ORDER BY  ife.fecha_emision,ifg.numero_guia, icom.id ) as item 
				                ,TO_CHAR(ife.fecha_emision, 'DD/MM/YYYY') AS date
				                ,ife.numero_invoice as invoice_number
				                ,ifg.numero_guia as guide_number
				                ,COALESCE(CAST(cli.id AS varchar),'') as  code_client
				                ,UPPER(COALESCE(cli.descripcion_negocio,''))  as client
				                ,pro.descripcion as product
				                ,COALESCE(ifgs3.monto_servicio,0) as derechos_exterior
				                ,COALESCE(ifgs3.monto_descuento,0) as discount  -- monto descuento derechos
				                ,COALESCE(ifg.monto_total,0) as total_invoice
				                ,COALESCE(ifga.total_acreditaciones,0) as total_acreditation
				                ,COALESCE(ifg.monto_total,0) - COALESCE(ifga.total_acreditaciones,0) AS 	total_fedex								
								 , COALESCE(icom.codigo_detalle_tcp,'') as	type_document
							  	 , COALESCE(icom.serie,'') as serie
							  	 , COALESCE(icom.correlativo,'')  as numero
							  	 , COALESCE(TO_CHAR(icom.fecha_emision , 'DD/MM/YYYY'),'') as fecha_emision 
							     , COALESCE(icom.codigo_moneda,'') as moneda 
							     , COALESCE(icom.valor_tipo_cambio,0) as valor_tipo_cambio	
                              	 , COALESCE(icom.monto_comprobante,0) as amount_payment 
							  	 , COALESCE(icom.monto_venta,0) as amount 
							  	 , COALESCE(icom.acumulado,0) as acumulado  
			                     , coalesce(dps5.descripcion,'') as description_status_guide
			                     , coalesce(ifg.observacion,'') as guide_observation
				                from 
					                nsf.invoice_fedex ife 
					                left join nsf.invoice_fedex_guia ifg on (ifg.id_invoice_fedex = ife.id )		
					                left join nsf.orden_facturable ofa on ( CAST(ifg.item  AS varchar) = ofa.numero_orden )
					                left join nsf.cliente cli on ofa.id_cliente = cli.id
					                left join nsf.producto pro on pro.id_producto = ifg.id_producto 
					                left join nsf.invoice_fedex_guia_servicio ifgs1 on ifgs1.id_invoice_fedex_guia =ifg.id  and ifgs1.id_servicio =(select CAST(codigo_nsf AS INT)  from nsf.invoice_fedex_homologacion where descripcion_fedex='FLETE')
					                left join nsf.invoice_fedex_guia_servicio ifgs2 on ifgs2.id_invoice_fedex_guia =ifg.id  and ifgs2.id_servicio =(select CAST(codigo_nsf AS INT) from nsf.invoice_fedex_homologacion where descripcion_fedex='SOBRECOSTO')
					                left join nsf.invoice_fedex_guia_servicio ifgs3 on ifgs3.id_invoice_fedex_guia =ifg.id  and ifgs3.id_servicio =(select CAST(codigo_nsf AS INT) from nsf.invoice_fedex_homologacion where descripcion_fedex='DERECHOS')
					                left join nsf.servicio 			   		  serv3	on (serv3.id = ifgs3.id_servicio) 
								    left join nsf.importe_comprobantes() 	  icom  on (ofa.id = icom.id_dof and serv3.id_categoria = icom.id_categoria)
								    left join nsf.detalle_parametro 		  dps3  on (ifgs3.id_estado = dps3.id AND dps3.codigo_general = 'ESTINVOICE')
								    left join nsf.detalle_parametro 		  dps4  on (ifgs3.id_estado = ofa.id_tipo_orden AND dps4.codigo_general = 'TIPDOCFAC' and dps4.codigo_detalle ='DOF')
                                    left join nsf.detalle_parametro 	      dps5  ON (ifg.id_estado 	= dps5.id AND dps5.codigo_general = 'ESTINVOICE')
					                left join (select 
								                      id_invoice_fedex_guia,sum(COALESCE(importe_no_pago,0)) as total_acreditaciones 
								                 from nsf.invoice_fedex_guia_acreditacion where estado = true
								                 Group by id_invoice_fedex_guia) ifga on ifga.id_invoice_fedex_guia = ifg.id
				                where   
					                dps3.codigo_detalle in ('PRO','VAL','VALOBS','VALPARC','ERROR','ACRE' ) -- Estado Servicio DERECHOS  
					                and ifg.id_estado in (select id from nsf.detalle_parametro where codigo_general='ESTINVOICE' and codigo_detalle in ('PRO','VAL','VALOBS','VALPARC','ERROR','ACRE')) 
					                and  DATE(ife.fecha_emision) BETWEEN DATE(@issue_Date_Start) AND DATE(@issue_Date_End)  
								    --------------------------------------------------------- TEST ---------------------------------------------------------
									--and  DATE(ife.fecha_emision) BETWEEN '2024-01-01' and '2024-05-28' 
					                order by  ife.fecha_emision,ifg.numero_guia, icom.id 
			            ) tb_informe_Fee;

 
                ";
                var arg = new { issue_Date_Start, issue_Date_End };
                var result = await connection.QueryAsync<ResponseReportInvoiceServiceFee>(query, arg);
                if (result.Any())
                {
                    // Crear DataSet
                    DataSet dataSet = new DataSet();

                    // Crear DataTable para la cabecera
                    DataTable headerTable = new DataTable("Header");
                    headerTable.Columns.Add("item", typeof(string));
                    headerTable.Columns.Add("date", typeof(string));
                    headerTable.Columns.Add("invoice_number", typeof(string));
                    headerTable.Columns.Add("guide_number", typeof(string));
                    headerTable.Columns.Add("description_status_guide", typeof(string));
                    headerTable.Columns.Add("code_client", typeof(string));
                    headerTable.Columns.Add("client", typeof(string));
                    headerTable.Columns.Add("product", typeof(string));
                    headerTable.Columns.Add("derechos_exterior", typeof(string));
                    headerTable.Columns.Add("discount", typeof(string));
                    headerTable.Columns.Add("total_invoice", typeof(string));
                    headerTable.Columns.Add("total_acreditation", typeof(string));
                    headerTable.Columns.Add("total_fedex", typeof(string));
                    headerTable.Columns.Add("type_document", typeof(string));
                    headerTable.Columns.Add("serie", typeof(string));
                    headerTable.Columns.Add("numero", typeof(string));
                    headerTable.Columns.Add("fecha_emision", typeof(string));
                    headerTable.Columns.Add("moneda", typeof(string));
                    headerTable.Columns.Add("valor_tipo_cambio", typeof(string));
                    headerTable.Columns.Add("amount_payment", typeof(string));
                    headerTable.Columns.Add("amount", typeof(string));
                    headerTable.Columns.Add("difference", typeof(string));
                    headerTable.Columns.Add("guide_observation", typeof(string));
                    // Añade más columnas de cabecera según sea necesario

                    // Agregar fila de cabecera
                    DataRow headerRow = headerTable.NewRow();
                    headerRow["item"] = "N°";
                    headerRow["date"] = "FECHA";
                    headerRow["invoice_number"] = "# INVOICE";
                    headerRow["guide_number"] = "# GUIA";
                    headerRow["description_status_guide"] = "ESTADO";
                    headerRow["code_client"] = "COD. CLIENTE";
                    headerRow["client"] = "CLIENTE";
                    headerRow["product"] = "PROD.";
                    headerRow["derechos_exterior"] = "DERECHOS DEL EXTERIOR";
                    headerRow["discount"] = "DSCTO";
                    headerRow["total_invoice"] = "TOTAL INVOICE";
                    headerRow["total_acreditation"] = "TOTAL ACREDIT.";
                    headerRow["total_fedex"] = "TOTAL FEDEX";
                    headerRow["type_document"] = "T. DOC";
                    headerRow["serie"] = "SERIE";
                    headerRow["numero"] = "NUMERO";
                    headerRow["fecha_emision"] = "FCH. EMI";
                    headerRow["moneda"] = "MONEDA";
                    headerRow["valor_tipo_cambio"] = "T/C";
                    headerRow["amount_payment"] = "IMPORTE COMPROBANTE";
                    headerRow["amount"] = "IMPORTE";
                    headerRow["difference"] = "DIFERENCIA";
                    headerRow["guide_observation"] = "OBSERVACIONES";
                    // Añade más valores de cabecera según sea necesario
                    headerTable.Rows.Add(headerRow);

                    // Agregar DataTable de cabecera al DataSet
                    dataSet.Tables.Add(headerTable);

                    // Crear un nuevo DataTable
                    DataTable contentTable = new DataTable();
                    contentTable.Columns.Add("item", typeof(int));
                    contentTable.Columns.Add("date", typeof(string));
                    contentTable.Columns.Add("invoice_number", typeof(string));
                    contentTable.Columns.Add("guide_number", typeof(string));
                    contentTable.Columns.Add("description_status_guide", typeof(string));
                    contentTable.Columns.Add("code_client", typeof(string));
                    contentTable.Columns.Add("client", typeof(string));
                    contentTable.Columns.Add("product", typeof(string));
                    contentTable.Columns.Add("derechos_exterior", typeof(decimal));
                    contentTable.Columns.Add("discount", typeof(decimal));
                    contentTable.Columns.Add("total_invoice", typeof(decimal));
                    contentTable.Columns.Add("total_acreditation", typeof(decimal));
                    contentTable.Columns.Add("total_fedex", typeof(decimal));
                    contentTable.Columns.Add("type_document", typeof(string));
                    contentTable.Columns.Add("serie", typeof(string));
                    contentTable.Columns.Add("numero", typeof(string));
                    contentTable.Columns.Add("fecha_emision", typeof(string));
                    contentTable.Columns.Add("moneda", typeof(string));
                    contentTable.Columns.Add("valor_tipo_cambio", typeof(decimal));
                    contentTable.Columns.Add("amount_payment", typeof(decimal));
                    contentTable.Columns.Add("amount", typeof(decimal));
                    contentTable.Columns.Add("difference", typeof(decimal));
                    contentTable.Columns.Add("guide_observation", typeof(string));


                    // Agregar cada objeto de ResponseInvoiceFedexHomologacion como una fila en el DataTable
                    foreach (var obj in result)
                    {
                        //dataTable.Rows.Add(obj.origin_description, obj.desc_type, obj.destination_id_nsf, obj.destination_id_nsc, obj.group_id, obj.born_from_the_invoice);
                        contentTable.Rows.Add(obj.item,
                                            obj.date,
                                             obj.invoice_number,
                                             obj.guide_number,
                                             obj.description_status_guide,
                                             obj.code_client,
                                             obj.client,
                                             obj.product,
                                             obj.derechos_exterior,
                                             obj.discount,
                                             obj.total_invoice,
                                             obj.total_acreditation,
                                             obj.total_fedex,
                                             obj.type_document,
                                             obj.serie,
                                             obj.numero,
                                             obj.fecha_emision,
                                             obj.moneda,
                                             obj.valor_tipo_cambio,
                                             obj.amount_payment,
                                             obj.amount,
                                             obj.difference,
                                             obj.guide_observation
                                             );
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
