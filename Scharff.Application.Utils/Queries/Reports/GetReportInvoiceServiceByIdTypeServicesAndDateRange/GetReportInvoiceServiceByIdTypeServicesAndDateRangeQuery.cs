using MediatR;
using Scharff.Domain.Response.Reports.GetReportInvoiceServiceByIdTypeServicesAndDateRangeQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Application.Queries.Reports.ReportInvoiceServiceByIdTypeServicesAndDateRange
{
    public class GetReportInvoiceServiceByIdTypeServicesAndDateRangeQuery : IRequest<ResponseFile>
    {
        public string codTypeReport { get; set; } 
        public DateTime issue_Date_Start  { get; set; }
        public DateTime issue_Date_End  { get; set; }
    }
}
