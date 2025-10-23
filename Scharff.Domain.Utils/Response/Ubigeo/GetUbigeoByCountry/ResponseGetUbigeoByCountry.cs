using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Domain.Response.Ubigeo.GetUbigeoByContry
{
    public class ResponseGetUbigeoByCountry
    {
        public int id { get; set; }
        public string? ubigeo_Code { get; set; }
        public string? description { get; set; }
        public string? country_Code { get; set; }
    }
}