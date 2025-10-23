using MediatR;
using Scharff.Domain.Response.Ubigeo.GetUbigeoByCode;
using Scharff.Domain.Utils.Exceptions;
using Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.GetUbigeoByCodeQuery;

namespace Scharff.Application.Queries.Ubigeo.GetUbigeoByCode
{
    public class GetUbigeoByCodeHandler : IRequestHandler<GetUbigeoByCodeQuery, List<ResponseGetUbigeoByCode>>
    {
        private readonly IGetUbigeoByCodeQuery _GetUbigeoByCodeQuery;

        public GetUbigeoByCodeHandler(IGetUbigeoByCodeQuery GetUbigeoByCodeQuery)
        {
            _GetUbigeoByCodeQuery = GetUbigeoByCodeQuery;
        }

        public async Task<List<ResponseGetUbigeoByCode>> Handle(GetUbigeoByCodeQuery request, CancellationToken cancellationToken)
        {
            var result = await _GetUbigeoByCodeQuery.GetUbigeoByCode(request.ubigeoCode);
            if (!result.Any())
            {
                throw new NotFoundException("No se encontro el ubigeo");
            }
            return result;
        }
    }
}
