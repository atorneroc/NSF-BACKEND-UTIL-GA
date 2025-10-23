using MediatR;
using Scharff.Domain.Response.Parameter.GetBusinessUnitByBranchOfficeId;

namespace Scharff.Application.Queries.Parameter.GetBusinessUnitByBranchOfficeId
{
    public class GetBusinessUnitByBranchOfficeIdQuery : IRequest<List<ResponseGetBusinessUnitByBranchOfficeId>>
    {
        public int CompanyId { get; set; }
        public int branchOfficeId { get; set; }
    }
}
