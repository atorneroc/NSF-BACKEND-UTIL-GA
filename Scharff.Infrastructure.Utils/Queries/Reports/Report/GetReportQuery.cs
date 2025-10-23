using Dapper;
using Npgsql;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Reports.Report
{
    public class GetReportQuery : IGetReportQuery
    {
        private readonly IDbConnection _connection;

        public GetReportQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<ResponseAllLiquidation>> GetReport()
        {
            using IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString);

            try
            {

                string sql = $@"  SELECT
	                            A.id,
	                            A.fecha_inicio as start_date,
	                            C.razon_social as company_name,
	                            D.descripcion as branch_office_name,
	                            E.descripcion as business_unit_name,
	                            F.descripcion as store_name,
	                            G.descripcion_negocio as client_name,
	                            H.codigo_detalle as status_code,
	                            H.descripcion as status_description,
                                I.descripcion_corta as currency_type,
                                A.monto_total as subtotal_price
                            FROM
	                            nsf.liquidacion 					A 												LEFT JOIN
	                            nsf.estructura_organizacional_base 	B ON B.id = A.id_estructura_organizacional_base LEFT JOIN
	                            nsf.empresa 						C ON C.id = B.id_empresa 						LEFT JOIN
	                            nsf.sucursal 						D ON D.id = B.id_sucursal 						LEFT JOIN	
	                            nsf.unidad_negocio 					E ON E.id = B.id_unidad_negocio 				LEFT JOIN
	                            nsf.producto 						F ON F.id_producto = B.id_producto 				LEFT JOIN
	                            nsf.cliente 						G ON G.id = A.id_cliente 						LEFT JOIN
	                            nsf.detalle_parametro 				H ON H.id = A.estado 							LEFT JOIN
	                            nsf.detalle_parametro 				I ON I.id = A.id_tipo_moneda
                            ORDER BY
	                            A.id desc
                            LIMIT
                                15;";

                IEnumerable<ResponseAllLiquidation> result = await connection.QueryAsync<ResponseAllLiquidation>(sql);

                return result.ToList();
            }
            catch (NpgsqlException err)
            {
                Console.WriteLine(err);
                throw;
            }
        }
    }

    public class ResponseAllLiquidation
    {
        public int Id { get; set; }
        public DateTime Start_Date { get; set; }
        public string Company_Name { get; set; } = string.Empty;
        public string Branch_Office_Name { get; set; } = string.Empty;
        public string Business_Unit_Name { get; set; } = string.Empty;
        public string Store_Name { get; set; } = string.Empty;
        public string Client_Name { get; set; } = string.Empty;
        public string Status_Code { get; set; } = string.Empty;
        public string Status_Description { get; set; } = string.Empty;
        public string Currency_Type { get; set; } = string.Empty;
        public decimal Subtotal_Price { get; set; }
    }
}
