using MediatR;
using Scharff.Domain.Response.Security.GetAccessByUser;

namespace Scharff.Application.Queries.Security.GetAccessByUser
{
    public class GetAccessByUserQuery : IRequest<List<ResponseAccess>>
    {
        public string User_Email { get; set; } = string.Empty;
    }
}
