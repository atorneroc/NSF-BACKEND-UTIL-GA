using Scharff.Domain.Response.Parameter.GetBusinessUnitByBranchOfficeId;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetBusinessUnitByBranchOfficeId
{
    public interface IGetBusinessUnitByBranchOfficeIdQuery
    {
        Task<List<ResponseGetBusinessUnitByBranchOfficeId>> GetBusinessUnitByBranchOfficeId(int CompanyId, int branchOfficeId);
    }
}
