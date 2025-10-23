using MediatR;
using Scharff.Domain.Response.User; 

namespace Scharff.Application.Queries.User.GetUserByProfileId
{
    public class GetAllUserByProfileIdQuery : IRequest<List<ResponseAllUserByProfileId>>
    {
        public string profile_Id { get; set; }
    }
}
