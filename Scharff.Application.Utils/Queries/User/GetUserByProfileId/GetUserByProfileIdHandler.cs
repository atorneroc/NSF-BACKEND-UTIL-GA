using MediatR;
using Scharff.Application.Queries.User.GetUserByProfileId;
using Scharff.Domain.Response.User;
using Scharff.Infrastructure.PostgreSQL.Queries.User.GetUserByProfileId;

namespace Scharff.Application.Queries.User.GetUserByProfile_id
{
    public class GetUserByProfileIdHandler : IRequestHandler<GetAllUserByProfileIdQuery, List<ResponseAllUserByProfileId>>
    {
        private readonly IGetUserByProfileIdQuery _getUserByProfileIdQuery;
        public GetUserByProfileIdHandler(IGetUserByProfileIdQuery getUserByProfileIdQuery)
        {
            _getUserByProfileIdQuery = getUserByProfileIdQuery;
        }

        public async Task<List<ResponseAllUserByProfileId>> Handle(GetAllUserByProfileIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _getUserByProfileIdQuery.GetUserByProfile(request.profile_Id);

            return result;
        }
    }
}
