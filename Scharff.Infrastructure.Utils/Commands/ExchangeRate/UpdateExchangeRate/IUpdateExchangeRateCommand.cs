using Scharff.Domain.Entities;

namespace Scharff.Infrastructure.PostgreSQL.Commands.ExchangeRate.UpdateExchangeRate
{
    public interface IUpdateExchangeRateCommand
    {
        Task<int> UpdateExchangeRate(ExchangeRateModel billableOrder);
    }
}
