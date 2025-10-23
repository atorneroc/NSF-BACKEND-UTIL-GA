using MediatR;
using Scharff.Domain.Response.Parameter.GetParameterByGroupId;

namespace Scharff.Application.Queries.Parameter.GetParameterByGroupId
{
    public class GetParameterByGroupIdQuery : IRequest<List<ResponseGetParameterByGroupId>>
    {
        public string groupId { get; set; } = string.Empty;
    }
}
