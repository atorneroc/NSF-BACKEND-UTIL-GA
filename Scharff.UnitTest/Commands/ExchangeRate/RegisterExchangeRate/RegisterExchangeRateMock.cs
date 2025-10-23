using Moq;
using Scharff.Domain.Entities;
using Scharff.Domain.Response.ExchangeRate.GetExchangeRateBroadCast;
using Scharff.Infrastructure.PostgreSQL.Commands.ExchangeRate.RegisterExchangeRate;
using Scharff.Infrastructure.PostgreSQL.Queries.ExchangeRate.GetExchangeRateBroadCast;

namespace Scharff.UnitTest.Commands.ExchangeRate.RegisterExchangeRate
{
    public static class RegisterExchangeRateMock
    {
        public static Mock<IRegisterExchangeRateCommand> RegisterExchangeRate(int expectedRegisterId)
        {
            var mock = new Mock<IRegisterExchangeRateCommand>();

            mock.Setup(x => x.RegisterExchangeRate(It.IsAny<ExchangeRateModel>()))
                .ReturnsAsync(expectedRegisterId);

            return mock;
        }

        public static Mock<IGetExchangeRateBroadCast> GetExchangeRateBroadCas()
        {
            var expectedExchange = new ResponseGetExchangeRateBroadCast();
            var mock = new Mock<IGetExchangeRateBroadCast>();

            mock.Setup(x => x.GetExchangeRateByBroadCast(DateTime.Now))
                .ReturnsAsync(expectedExchange);

            return mock;
        }
    }
}
