using Scharff.Domain.Response.Security.GetAccessByUser;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Security.GetAccessByUser
{
    public interface IGetAccessByUserQuery
    {
        Task<List<GetAccessByUserResponse>> GetAccessByUser(string user_email);
    }
}
