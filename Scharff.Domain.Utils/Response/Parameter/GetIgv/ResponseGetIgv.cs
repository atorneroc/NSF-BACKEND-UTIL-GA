using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Domain.Response.Parameter.GetIgv
{
    public class ResponseGetIgv
    {
        public string? start_month { get; set; }
        public string? start_year { get; set; }
        public decimal current_value { get; set; }
    }
}
