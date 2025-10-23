
using MediatR;
using Scharff.Domain.Response.Parameter.GetStoreByBusinessUnitId;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetStoreByBusinessUnitId;

namespace Scharff.Application.Queries.Parameter.GetStoreByBusinessUnitId
{
    public class GetStoreByBusinessUnitIdHandler : IRequestHandler<GetStoreByBusinessUnitIdQuery, List<ResponseGetStoreByBusinessUnitId>>
    {
        private readonly IGetStoreByBusinessUnitIdQuery _GetStoreByBusinessUnitIdQuery;
        public GetStoreByBusinessUnitIdHandler(IGetStoreByBusinessUnitIdQuery GetStoreByBusinessUnitIdQuery)
        {
            _GetStoreByBusinessUnitIdQuery = GetStoreByBusinessUnitIdQuery;
        }

        public async Task<List<ResponseGetStoreByBusinessUnitId>> Handle(GetStoreByBusinessUnitIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _GetStoreByBusinessUnitIdQuery.GetStoreByBusinessUnitId(request.CompanyId, request.branchOfficeId, request.businessUnitId);
            return result;
        }
    }
}
