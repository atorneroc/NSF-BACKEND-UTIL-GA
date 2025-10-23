using MediatR;
using Scharff.Domain.Response.ExchangeRate.GetAllExchangeRate;
using Scharff.Domain.Response.Utils;

namespace Scharff.Application.Queries.ExchangeRate.GetAllExchangeRate
{
    public class GetAllExchangeRateQuery : IRequest<PaginatedResponse<ResponseGetAllExchangeRate>>
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public DateTime? date_change { get; set; }
    }
}
