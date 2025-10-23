using Scharff.Domain.Response.BillingCourt;

namespace Scharff.Infrastructure.PostgreSQL.Queries.BillingCourt.GetProductByIdBusinessUnitIdCompany
{
    public interface IGetProductByIdBusinessUnitIdCompanyQuery
    {
        Task<List<ResponseGetProductByIdBusinessUnitIdCompany>> GetProductByIdBusinessUnitIdCompany(int id_company, int id_business_unit);
    }
}
