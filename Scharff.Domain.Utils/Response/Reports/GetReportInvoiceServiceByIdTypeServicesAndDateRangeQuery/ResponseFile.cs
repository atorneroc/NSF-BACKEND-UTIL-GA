using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Domain.Response.Reports.GetReportInvoiceServiceByIdTypeServicesAndDateRangeQuery
{
    public class ResponseFile
    {
        public string? Name { get; set; }
        public byte[]? File { get; set; }
        public string? Type { get; set; }
    }
}
