using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Domain.Entities
{
    public class ProductModel
    {
        public int id { get; set; }
        public string descripcion { get; set; } = string.Empty;
        public string usuario_creacion { get; set; } = string.Empty;
        public DateTime fecha_creacion { get; set; } 
        public string? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public string? estado { get; set; } 

    }
}
