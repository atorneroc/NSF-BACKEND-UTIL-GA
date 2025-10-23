using MediatR;
using Scharff.Domain.Response.Reports.GetConsolidatedReport;

namespace Scharff.Application.Queries.Reports.GetConsolidatedReport
{
    public class GetConsolidatedReportQuery : IRequest<ResponseConsolidatedReport>
    {
        public int service_order_id;
    }
}