using Scharff.Domain.Request.Report;
using Scharff.Infrastructure.PostgreSQL.Queries.Reports.Report;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Reports.GetReportServiceOrderCourier
{
    public interface IGetReportServiceOrderCourierQuery
    {
        Task<DataSet> GetReportServiceOrderCourier(RequestReportServiceOrderCourier request);
    }
}


