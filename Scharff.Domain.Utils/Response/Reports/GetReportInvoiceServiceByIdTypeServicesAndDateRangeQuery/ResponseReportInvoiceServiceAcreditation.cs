using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Domain.Response.Reports.GetReportInvoiceServiceByIdTypeServicesAndDateRangeQuery
{
    public class ResponseReportInvoiceServiceAcreditation
    {
        public string item { get; set; } = string.Empty;
        public string concept { get; set; } = string.Empty;
        public string date_acreditations { get; set; } = string.Empty;
        public string code_acreditations { get; set; } = string.Empty; 
        public string case_number { get; set; } = string.Empty; 
        public string invoice_number { get; set; } = string.Empty; 
        public string guide_number { get; set; } = string.Empty; 
        public string code_client { get; set; } = string.Empty; 
        public string client { get; set; } = string.Empty; 
        public string product { get; set; } = string.Empty; 
        public decimal freight { get; set; }  
        public decimal fuel { get; set; }  
        public decimal other { get; set; } 
        public decimal discount { get; set; } 
        public decimal total { get; set; } 
        public decimal no_payment { get; set; }  
        public decimal invoice_difference { get; set; } 
        public string recorder { get; set; } = string.Empty; 
        public string observations { get; set; } = string.Empty; 
 
    }
}
