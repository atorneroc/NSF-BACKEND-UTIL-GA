using MediatR;
using Scharff.Domain.Response.UbicacionGeografica;
using Scharff.Infrastructure.PostgreSQL.Queries.Product.GetProductsByIdEmpresaAndIdSucursalAndIdUNegocio;
using Scharff.Infrastructure.PostgreSQL.Queries.UbicacionGeografica.GetUbicacionGeograficaById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Application.Queries.UbicacionGeografica.GetUbicacionGeograficaById
{
    public class GetUbicacionGeograficaByIdHandler : IRequestHandler<GetUbicacionGeograficaByIdQuery, List<ResponseGetUbicacionGeograficaById>>
    {
        private readonly IGetUbicacionGeograficaByIdQuery _Repository;

        public GetUbicacionGeograficaByIdHandler(IGetUbicacionGeograficaByIdQuery Repository)
        {
            _Repository = Repository;
        }

        public async Task<List<ResponseGetUbicacionGeograficaById>> Handle(GetUbicacionGeograficaByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _Repository.GetUbicacionGeograficaById(request.id);
            return result;
        }
    }
}
