using Scharff.Domain.Response.Parameter.GetParameterByCodeDetail;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetParameterByDetailCode
{
    public interface IGetParameterByDetailCodeQuery
    {
        Task<List<ResponseGetParameterByDetailCode>> GetParameterByCodeDetail(List<string> lstCodes);
    }
}