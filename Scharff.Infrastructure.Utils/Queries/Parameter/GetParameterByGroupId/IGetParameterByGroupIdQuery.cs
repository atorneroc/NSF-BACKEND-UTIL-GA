using Scharff.Domain.Response.Parameter.GetParameterByGroupId;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetParameterByGroupId
{
    public interface IGetParameterByGroupIdQuery
    {
        Task<List<ResponseGetParameterByGroupId>> GetParameterByGroupId(string groupId);
    }
}
