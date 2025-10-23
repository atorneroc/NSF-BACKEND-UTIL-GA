using MediatR;
using Scharff.Domain.Response.Ubigeo.GetAllCountryRegion;
using Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.GetAllCountryRegion;

namespace Scharff.Application.Queries.Parameter.GetAllCountryRegion
{
    public class GetAllCountryRegionHandler : IRequestHandler<GetAllCountryRegionQuery, List<ResponseGetAllCountryRegion>>
    {
        private readonly IGetAllCountryRegionQuery _GetAllCountryRegionQuery;

        public GetAllCountryRegionHandler(IGetAllCountryRegionQuery GetAllCountryRegionQuery)
        {
            _GetAllCountryRegionQuery = GetAllCountryRegionQuery;
        }

        public async Task<List<ResponseGetAllCountryRegion>> Handle(GetAllCountryRegionQuery request, CancellationToken cancellationToken)
        {
            var result = await _GetAllCountryRegionQuery.GetAllCountryRegion(request.Term);           

            return result;
        }
    }
}
