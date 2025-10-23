using Scharff.Domain.Response.Reports.GetConsolidatedReport;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Reports.GetConsolidatedReport
{
    public interface IGetConsolidatedReport
    {
        Task<ResponseConsolidatedReport> GetConsolidatedReportByServiceOrderId(int service_order_id);
    }
}