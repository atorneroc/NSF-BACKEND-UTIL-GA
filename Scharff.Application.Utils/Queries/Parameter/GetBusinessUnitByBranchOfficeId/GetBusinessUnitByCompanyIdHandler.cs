using MediatR;
using Scharff.Domain.Response.Parameter.GetBusinessUnitByBranchOfficeId;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetBusinessUnitByBranchOfficeId;

namespace Scharff.Application.Queries.Parameter.GetBusinessUnitByBranchOfficeId
{
    public class GetBusinessUnitByBranchOfficeIdHandler : IRequestHandler<GetBusinessUnitByBranchOfficeIdQuery, List<ResponseGetBusinessUnitByBranchOfficeId>>
    {
        private readonly IGetBusinessUnitByBranchOfficeIdQuery _GetBusinessUnitByBranchOfficeIdQuery;

        public GetBusinessUnitByBranchOfficeIdHandler(IGetBusinessUnitByBranchOfficeIdQuery GetBusinessUnitByBranchOfficeIdQuery)
        {
            _GetBusinessUnitByBranchOfficeIdQuery = GetBusinessUnitByBranchOfficeIdQuery;
        }

        public async Task<List<ResponseGetBusinessUnitByBranchOfficeId>> Handle(GetBusinessUnitByBranchOfficeIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _GetBusinessUnitByBranchOfficeIdQuery.GetBusinessUnitByBranchOfficeId(request.CompanyId, request.branchOfficeId);

            return result;
        }
    }
}
