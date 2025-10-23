using MediatR;
using Scharff.Domain.Response.Ubigeo.GetUbigeoByContry;
using Scharff.Domain.Utils.Exceptions;
using Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.GetUbigeoByCountryQuery;

namespace Scharff.Application.Queries.Ubigeo.GetUbigeoByCountry
{
    public class GetUbigeoByCountryHandler : IRequestHandler<GetUbigeoByCountryQuery, List<ResponseGetUbigeoByCountry>>
    {
        private readonly IGetUbigeoByCountryQuery _getUbigeoByCountryQuery;
        public GetUbigeoByCountryHandler(IGetUbigeoByCountryQuery getUbigeoByCountryQuery)
        {
            _getUbigeoByCountryQuery = getUbigeoByCountryQuery;
        }
        public async Task<List<ResponseGetUbigeoByCountry>> Handle(GetUbigeoByCountryQuery request, CancellationToken cancellationToken)
        {
            var result = await _getUbigeoByCountryQuery.GetUbigeoByCountry(request.term, request.size);

            if (!result.Any())
            {
                throw new NotFoundException($"Ubigeo con el termino: {request.term}, no encontrados");
            }

            return result;
        }
    }

}
