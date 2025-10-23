
namespace Scharff.Domain.Response.Reports.GetConsolidatedReport
{
    public class ResponseConsolidatedReport
    {
        public int Id { get; set; }
        public int company_id { get; set; } // falta de tabla compañia
        public int client_id { get; set; }
        public string author { get; set; }
        public int service_category_id { get; set; }
        public int category_id_invoice { get; set; }
        public List<GuideModel> guide_gist { get; set; }
    }

    public class GuideModel
    {
        public int billable_order_id { get; set; }
    }
}
