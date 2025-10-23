using MediatR;
using Scharff.Domain.Response.Parameter.GetParameterByCodeDetail;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetParameterByDetailCode;

namespace Scharff.Application.Queries.Parameter.GetParameterByDetailCode
{
    public class GetParameterByDetailCodeHandler : IRequestHandler<GetParameterByDetailCodeQuery, List<ResponseGetParameterByDetailCode>>
    {
        private readonly IGetParameterByDetailCodeQuery _getParameterByDetailCodeQuery;

        public GetParameterByDetailCodeHandler(IGetParameterByDetailCodeQuery getParameterByDetailCodeQuery)
        {
            _getParameterByDetailCodeQuery = getParameterByDetailCodeQuery;
        }
        public async Task<List<ResponseGetParameterByDetailCode>> Handle(GetParameterByDetailCodeQuery request, CancellationToken cancellationToken)
        {
            var result = await _getParameterByDetailCodeQuery.GetParameterByCodeDetail(request.Lst_Codes);

            return result;
        }
    }
}
