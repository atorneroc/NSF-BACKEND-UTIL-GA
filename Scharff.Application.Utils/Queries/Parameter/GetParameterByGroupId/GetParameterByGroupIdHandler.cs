using MediatR;
using Scharff.Domain.Response.Parameter.GetParameterByGroupId;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetParameterByGroupId;

namespace Scharff.Application.Queries.Parameter.GetParameterByGroupId
{
    public class GetParameterByGroupIdHandler : IRequestHandler<GetParameterByGroupIdQuery, List<ResponseGetParameterByGroupId>>
    {
        private readonly IGetParameterByGroupIdQuery _getParameterByGroupIdQuery;

        public GetParameterByGroupIdHandler(IGetParameterByGroupIdQuery getParameterByGroupIdQuery)
        {
            _getParameterByGroupIdQuery = getParameterByGroupIdQuery;
        }

        public async Task<List<ResponseGetParameterByGroupId>> Handle(GetParameterByGroupIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _getParameterByGroupIdQuery.GetParameterByGroupId(request.groupId);



            return result;
        }
    }
}
