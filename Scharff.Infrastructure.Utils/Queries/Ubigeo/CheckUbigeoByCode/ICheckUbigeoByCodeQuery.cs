using Scharff.Domain.Response.Ubigeo.CheckUbigeoByCode;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.CheckUbigeoByCode
{
    public interface ICheckUbigeoByCodeQuery
    {
        Task<List<ResponseCheckUbigeoByCode>> CheckUbigeoByCode(string ubigeoCode);
    }
}