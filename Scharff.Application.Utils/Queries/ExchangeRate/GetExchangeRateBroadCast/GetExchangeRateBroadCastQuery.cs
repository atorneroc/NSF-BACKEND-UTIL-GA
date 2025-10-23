using MediatR;
using Scharff.Domain.Response.ExchangeRate.GetExchangeRateBroadCast;

namespace Scharff.Application.Queries.ExchangeRate.GetExchangeRateBroadCast
{
    public class GetExchangeRateBroadCastQuery : IRequest<ResponseGetExchangeRateBroadCast>
    {
        public DateTime broadCast { get; set; }
    }
}
