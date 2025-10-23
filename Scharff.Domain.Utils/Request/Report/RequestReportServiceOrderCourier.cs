using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Domain.Request.Report
{
    public class RequestReportServiceOrderCourier
    {
        public int? id { get; set; }
        public int? company_id { get; set; }
        public int? client_id { get; set; }
        public string? author { get; set; } =  String.Empty;
        public int? service_category_id { get; set; }
        public int? category_id_invoice { get; set; }
        public List<Guide> ? guide_list { get; set; } = new List<Guide>();
    }
    public class Guide
    {
        public int billable_order_id { get; set; }
    }


}
