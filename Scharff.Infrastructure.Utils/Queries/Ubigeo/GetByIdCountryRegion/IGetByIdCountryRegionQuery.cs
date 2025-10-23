using Scharff.Domain.Response.Ubigeo.GetByIdCountryRegion;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.GetByIdCountryRegion
{
    public interface IGetByIdCountryRegionQuery
    {
        Task<List<ResponseGetByIdCountryRegion>> GetByIdCountryRegion(int id);
    }
}
