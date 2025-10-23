using Scharff.Domain.Response.Company.GetSunatRegimenById;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Company.GetSunatRegimenById
{
    public interface IGetSunatRegimenByIdQuery
    {
        Task<ResponseGetSunatRegimenById> GetSunatRegimenById(int idCompany);
    }
}