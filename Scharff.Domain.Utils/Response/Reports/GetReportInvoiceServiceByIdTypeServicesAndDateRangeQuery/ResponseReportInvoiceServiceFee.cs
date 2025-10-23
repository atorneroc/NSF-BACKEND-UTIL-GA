using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Domain.Response.Reports.GetReportInvoiceServiceByIdTypeServicesAndDateRangeQuery
{
    public class ResponseReportInvoiceServiceFee
    {  
        public int item { get; set; }
        public string date { get; set; } = string.Empty;
        public string invoice_number { get; set; } = string.Empty;
        public string guide_number { get; set; } = string.Empty;
        public string code_client { get; set; } = string.Empty;
        public string client { get; set; } = string.Empty;
        public string product { get; set; } = string.Empty;
        public decimal derechos_exterior { get; set; } 
        public decimal discount { get; set; } 
        public decimal total_invoice { get; set; } 
        public decimal total_acreditation { get; set; } 
        public decimal total_fedex { get; set; }
        public string type_document { get; set; } = string.Empty;
        public string serie { get; set; } = string.Empty;
        public string numero { get; set; } = string.Empty;
        public string fecha_emision { get; set; } = string.Empty;
        public string moneda { get; set; } = string.Empty;
        public decimal valor_tipo_cambio { get; set; }
        public decimal amount_payment { get; set; }
        public decimal amount { get; set; }
        public decimal difference { get; set; }
        public string description_status_guide { get; set; } = string.Empty;
        public string guide_observation { get; set; } = string.Empty;


    }
}
