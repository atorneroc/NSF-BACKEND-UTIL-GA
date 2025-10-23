using Scharff.Domain.Response.ExchangeRate.GetExchangeRateBroadCast;

namespace Scharff.Infrastructure.PostgreSQL.Queries.ExchangeRate.GetExchangeRateBroadCast
{
    public interface IGetExchangeRateBroadCast
    {
        Task<ResponseGetExchangeRateBroadCast> GetExchangeRateByBroadCast(DateTime broadCast);
    }
}