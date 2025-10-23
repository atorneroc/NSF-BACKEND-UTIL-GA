using MediatR;
using Scharff.Application.Queries.Parameter.GetAllCountryRegion;
using Scharff.Domain.Response.Ubigeo.GetByIdCountryRegion;
using Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.GetAllCountryRegion;
using Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.GetByIdCountryRegion;

namespace Scharff.Application.Queries.Ubigeo.GetCountryRegionById
{
    public class GetCountryRegionByIdHandler : IRequestHandler<GetCountryRegionByIdQuery, List<ResponseGetByIdCountryRegion>>
    {
        private readonly IGetByIdCountryRegionQuery _GetByIdCountryRegionQuery;

        public GetCountryRegionByIdHandler(IGetByIdCountryRegionQuery GetByIdCountryRegionQuery)
        {
            _GetByIdCountryRegionQuery = GetByIdCountryRegionQuery;
        }
        public  async Task<List<ResponseGetByIdCountryRegion>> Handle(GetCountryRegionByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _GetByIdCountryRegionQuery.GetByIdCountryRegion(request.id);

            return result;
        }
    }
}
