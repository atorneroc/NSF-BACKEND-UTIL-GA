using MediatR;
using Scharff.Domain.Response.Service.GetAllService;
using Scharff.Infrastructure.PostgreSQL.Queries.Service.GetAllService;

namespace Scharff.Application.Queries.Service.GetAllService
{
    public class GetAllServiceHandler : IRequestHandler<GetAllServiceQuery, List<ResponseGetAllService>>
    {
        private readonly IGetAllServiceQuery _GetAllServiceQuery;

        public GetAllServiceHandler(IGetAllServiceQuery GetAllServiceQuery)
        {
            _GetAllServiceQuery = GetAllServiceQuery;
        }

        public async Task<List<ResponseGetAllService>> Handle(GetAllServiceQuery request, CancellationToken cancellationToken)
        {
            var result = await _GetAllServiceQuery.GetAllService();           

            return result;
        }
    }
}
