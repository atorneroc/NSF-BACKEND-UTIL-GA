using Scharff.Domain.Response.Ubigeo.GetUbigeoByContry;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.GetUbigeoByCountryQuery
{
    public interface IGetUbigeoByCountryQuery
    {
        Task<List<ResponseGetUbigeoByCountry>> GetUbigeoByCountry(string? term, int size);
    }
}
