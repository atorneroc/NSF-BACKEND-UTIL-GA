using Scharff.Domain.Entities;

namespace Scharff.Infrastructure.PostgreSQL.Commands.ExchangeRate.RegisterExchangeRate
{
    public interface IRegisterExchangeRateCommand
    {
        Task<int> RegisterExchangeRate(ExchangeRateModel billableOrder);
    }
}
