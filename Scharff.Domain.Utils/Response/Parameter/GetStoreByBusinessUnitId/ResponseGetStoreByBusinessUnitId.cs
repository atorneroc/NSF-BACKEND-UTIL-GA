using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Domain.Response.Parameter.GetStoreByBusinessUnitId
{
    public class ResponseGetStoreByBusinessUnitId
    {
        public string Name { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Id_estructura_base { get; set; }
        public string Integration_code { get; set; } = string.Empty;
    }
}
