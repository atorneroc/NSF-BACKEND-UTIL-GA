using MediatR;
using Scharff.Domain.Response.ExchangeRate.GetAllExchangeRate;
using Scharff.Domain.Response.Utils;
using Scharff.Infrastructure.PostgreSQL.Queries.ExchangeRate.GetAllExchangeRate;

namespace Scharff.Application.Queries.ExchangeRate.GetAllExchangeRate
{
    public class GetAllExchangeRateHandler : IRequestHandler<GetAllExchangeRateQuery, PaginatedResponse<ResponseGetAllExchangeRate>>
    {
        private readonly IGetAllExchangeRateQuery _getAllExchangeRate;

        public GetAllExchangeRateHandler(IGetAllExchangeRateQuery getAllExchangeRate)
        {
            _getAllExchangeRate = getAllExchangeRate;
        }
        public async Task<PaginatedResponse<ResponseGetAllExchangeRate>> Handle(GetAllExchangeRateQuery request, CancellationToken cancellationToken)
        {
            var result = await _getAllExchangeRate.GetAllExchangeRate(request.pageNumber,request.pageSize,request.date_change);
            return result;
        }
    }
}
