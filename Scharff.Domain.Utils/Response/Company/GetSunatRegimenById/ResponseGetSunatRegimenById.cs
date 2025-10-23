using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Domain.Response.Company.GetSunatRegimenById
{
    public class ResponseGetSunatRegimenById
    {
        public int Id { get; set; }
        public string Company_name { get; set; } = string.Empty;
        public bool Sunat_Regimen { get; set; } 
        public string? current_account_number { get; set; } = string.Empty;
    }
}
