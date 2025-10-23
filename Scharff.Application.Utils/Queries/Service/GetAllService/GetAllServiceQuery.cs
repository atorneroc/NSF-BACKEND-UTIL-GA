using MediatR;
using Scharff.Domain.Response.Service.GetAllService;

namespace Scharff.Application.Queries.Service.GetAllService
{
    public class GetAllServiceQuery : IRequest<List<ResponseGetAllService>>{}
}
