
using Scharff.Domain.Response.Reports.GetReportInvoiceServiceByIdTypeServicesAndDateRangeQuery;
using System.Data;

namespace Scharff.Infrastructure.Http.Queries.Reports.GetReportInvoiceServiceByIdTypeServicesAndDateRange
{
    public interface IGetReportInvoiceServiceByIdTypeServicesAndDateRange
    {


        Task<DataSet> GetReportAcreditations(DateTime issue_Date_Start, DateTime issue_Date_End); 
        Task<DataSet> GetReportfreight( DateTime issue_Date_Start, DateTime issue_Date_End);
        Task<DataSet> GetReportFee( DateTime issue_Date_Start, DateTime issue_Date_End); 

    }
}
