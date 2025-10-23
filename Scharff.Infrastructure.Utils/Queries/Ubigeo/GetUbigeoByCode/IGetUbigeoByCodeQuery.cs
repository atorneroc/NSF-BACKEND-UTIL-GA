using Scharff.Domain.Response.Ubigeo.GetUbigeoByCode;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.GetUbigeoByCodeQuery
{
    public interface IGetUbigeoByCodeQuery
    {
        Task<List<ResponseGetUbigeoByCode>> GetUbigeoByCode(List<string> ubigeoCode);
    }
}
