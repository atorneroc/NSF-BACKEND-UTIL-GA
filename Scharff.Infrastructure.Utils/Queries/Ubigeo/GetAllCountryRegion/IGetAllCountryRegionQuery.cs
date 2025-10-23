using Scharff.Domain.Response.Ubigeo.GetAllCountryRegion;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.GetAllCountryRegion
{
    public interface IGetAllCountryRegionQuery
    {
        Task<List<ResponseGetAllCountryRegion>> GetAllCountryRegion(string? Term); 
    }
}
