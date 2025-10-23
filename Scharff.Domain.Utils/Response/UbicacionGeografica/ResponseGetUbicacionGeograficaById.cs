using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Domain.Response.UbicacionGeografica
{
    public class ResponseGetUbicacionGeograficaById
    {
        public int Id { get; set; }
        public string? pais { get; set; } = string.Empty;
        public string? codigo_pais { get; set; } = string.Empty;

    }
}
