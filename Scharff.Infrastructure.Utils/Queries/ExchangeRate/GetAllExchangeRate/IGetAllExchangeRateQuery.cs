using Scharff.Domain.Response.ExchangeRate.GetAllExchangeRate;
using Scharff.Domain.Response.Utils;

namespace Scharff.Infrastructure.PostgreSQL.Queries.ExchangeRate.GetAllExchangeRate
{
    public interface IGetAllExchangeRateQuery
    {
        Task<PaginatedResponse<ResponseGetAllExchangeRate>> GetAllExchangeRate(int pageNumber, int pageSize,DateTime? date_change);
    }
}
