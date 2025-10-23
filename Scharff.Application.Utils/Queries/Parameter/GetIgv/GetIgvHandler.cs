using MediatR;
using Scharff.Domain.Response.Parameter.GetIgv;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetIgv;

namespace Scharff.Application.Queries.Parameter.GetIgv
{
    public class GetIgvHandler : IRequestHandler<GetIgvQuery, List<ResponseGetIgv>>
    {
        private readonly IGetIgvQuery _getIgvQuery;

        public GetIgvHandler(IGetIgvQuery getIgvQuery)
        {
            _getIgvQuery = getIgvQuery;
        }

        public async Task<List<ResponseGetIgv>> Handle(GetIgvQuery request, CancellationToken cancellationToken)
        {
            var result = await _getIgvQuery.GetIgv();

            return result;
        }
    }
}
