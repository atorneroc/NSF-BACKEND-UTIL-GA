using Scharff.Domain.Response.BillingCourt;

namespace Scharff.Infrastructure.PostgreSQL.Queries.BillingCourt.GetBusinessUnitByIdCompany
{
    public interface IGetBusinessUnitByIdCompanyQuery
    {
        Task<List<ResponseGetBusinessUnitByIdCompany>> GetBusinessUnitByIdCompany(int id_company);
    }
}
