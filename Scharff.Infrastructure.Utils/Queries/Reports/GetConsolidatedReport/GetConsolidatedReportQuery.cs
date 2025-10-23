

using Dapper;
using Npgsql;
using Scharff.Domain.Response.Reports.GetConsolidatedReport;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Reports.GetConsolidatedReport
{
    public class GetConsolidatedReportQuery : IGetConsolidatedReport
    {
        private readonly IDbConnection _connection;

        public GetConsolidatedReportQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<ResponseConsolidatedReport> GetConsolidatedReportByServiceOrderId(int service_order_id)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString))
            {

                try
                {
                    string query = @"
                        SELECT 
                            osc.id AS Id,
                            eob.id_empresa AS company_id,  
                            osc.id_cliente_cliente_final AS client_id,
                            COALESCE(substring(osc.usuario_creacion FROM 1 FOR position('@' IN osc.usuario_creacion) - 1), '') AS author,
                            osc.id_categoria_servicio AS service_category_id,
                            osc.id_categoria_invoice AS category_id_invoice,
                            oscg.id_orden_facturable AS billable_order_id
                        FROM nsf.orden_servicio_courier osc
                        LEFT JOIN nsf.estructura_organizacional_base eob ON osc.id_estructura_organizacional = eob.id
                        LEFT JOIN nsf.orden_servicio_courier_guia oscg ON osc.id = oscg.id_orden_servicio_courier
                        WHERE osc.id = @ServiceOrderId";

                    var parameters = new { ServiceOrderId = service_order_id };
                    var lookup = new Dictionary<int, ResponseConsolidatedReport>();
                    var result = await connection.QueryAsync<ResponseConsolidatedReport, GuideModel, ResponseConsolidatedReport>(
                        query,
                        (report, guide) =>
                        {
                            if (!lookup.TryGetValue(report.Id, out var consolidatedReport))
                            {
                                consolidatedReport = report;
                                consolidatedReport.guide_gist = new List<GuideModel>();
                                lookup.Add(consolidatedReport.Id, consolidatedReport);
                            }
                            consolidatedReport.guide_gist.Add(guide);
                            return consolidatedReport;
                        },
                        parameters,
                        splitOn: "billable_order_id"
                    );

                    return lookup.Values.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener el reporte consolidado", ex);
                }
            }

        }
    }
}
