using MediatR;
using Scharff.Domain.Response.Reports.GetConsolidatedReport;
using Scharff.Infrastructure.PostgreSQL.Queries.Reports.GetConsolidatedReport;

namespace Scharff.Application.Queries.Reports.GetConsolidatedReport
{
    public class GetConsolidatedReportHandler : IRequestHandler<GetConsolidatedReportQuery, ResponseConsolidatedReport>
    {
        private readonly IGetConsolidatedReport _getConsolidatedReportByServiceOrderCourierId;

        public GetConsolidatedReportHandler(IGetConsolidatedReport getConsolidatedReportByServiceOrderId)
        {
            _getConsolidatedReportByServiceOrderCourierId = getConsolidatedReportByServiceOrderId;
        }

        public async Task<ResponseConsolidatedReport> Handle(GetConsolidatedReportQuery request, CancellationToken cancellationToken)
        {
            var result = await _getConsolidatedReportByServiceOrderCourierId.GetConsolidatedReportByServiceOrderId(request.service_order_id);

            return result;
        }
    }
}