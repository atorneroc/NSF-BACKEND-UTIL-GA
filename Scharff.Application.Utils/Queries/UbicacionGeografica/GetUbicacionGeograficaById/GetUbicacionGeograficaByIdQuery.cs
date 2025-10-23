using MediatR;
using Scharff.Domain.Response.UbicacionGeografica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Application.Queries.UbicacionGeografica.GetUbicacionGeograficaById
{
    public class GetUbicacionGeograficaByIdQuery : IRequest<List<ResponseGetUbicacionGeograficaById>>
    {
        public int id { get; set; }
    }
}
