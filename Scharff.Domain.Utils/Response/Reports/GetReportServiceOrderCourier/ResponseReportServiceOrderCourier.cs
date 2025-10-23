using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Domain.Response.Reports.GetReportServiceOrderCourier
{
    public class ResponseReportServiceOrderCourier
    { 
        public int item { get; set; } 
        public string guide_date { get; set; } = string.Empty;
        public string package_description { get; set; } = string.Empty;
        public string sender_contact { get; set; } = string.Empty;
        public string destination_city_code { get; set; } = string.Empty;
        public string package_quantity { get; set; } = string.Empty;
        public string package_weight { get; set; } = string.Empty;
        public string package_weight_volumetric { get; set; } = string.Empty;        
        public string packaging_type { get; set; } = string.Empty;
        public string billable_order_id { get; set; } = string.Empty;
        public string company { get; set; } = string.Empty;
        public string no_awb { get; set; } = string.Empty;
        public string usd_tarifa { get; set; } = string.Empty;
        public string discount_percentage { get; set; } = string.Empty;
        public string discount_amount { get; set; } = string.Empty;
        public string subtotal_discount_amount { get; set; } = string.Empty;
        public string usd_descuento { get; set; } = string.Empty;
        public string usd_fuel { get; set; } = string.Empty;
        public string acreditacion { get; set; } = string.Empty;
        public string usd_descuento_global { get; set; } = string.Empty;
        public string usd_neto { get; set; } = string.Empty;
        public string igv_descuento_global { get; set; } = string.Empty;

        public string client_id { get; set; } = string.Empty;
        public string company_name { get; set; } = string.Empty;
        public string identity_document_number { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public string Fax { get; set; } = string.Empty; 
        public string client_address { get; set; } = string.Empty; 



    }
}
