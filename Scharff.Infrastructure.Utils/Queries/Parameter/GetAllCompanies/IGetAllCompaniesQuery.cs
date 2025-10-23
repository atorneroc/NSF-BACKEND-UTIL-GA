using Scharff.Domain.Response.Parameter.GetAllCompanies;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetAllCompanies
{
    public interface IGetAllCompaniesQuery
    {
        Task<List<ResponseGetAllCompanies>> GetAllCompanies();
    }
}
