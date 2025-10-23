using Scharff.Domain.Response.Ubigeo.CheckUbigeoBySapCodes;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.CheckUbigeoBySapCodes
{
    public interface ICheckUbigeoBySapCodesQuery
    {
        Task<List<ResponseUbigeoLocationBySapCodes>> CheckUbigeoBySapCodes(string sapCountryCode, string sapRegionCode);
    }
}
