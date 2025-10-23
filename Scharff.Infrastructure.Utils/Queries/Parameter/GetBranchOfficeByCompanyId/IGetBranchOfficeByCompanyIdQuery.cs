using Scharff.Domain.Response.Parameter.GetBranchOfficeByCompanyId;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetBranchOfficeByCompanyId
{
    public interface IGetBranchOfficeByCompanyIdQuery
    {
        Task<List<ResponseGetBranchOfficeByCompanyId>> GetBranchOfficeByCompanyId(int CompanyId);
    }
}
