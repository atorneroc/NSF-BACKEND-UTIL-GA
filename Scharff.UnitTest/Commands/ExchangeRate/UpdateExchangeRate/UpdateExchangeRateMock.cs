using Moq;
using Scharff.Application.Queries.ExchangeRate.GetExchangeRateBroadCast;
using Scharff.Domain.Entities;
using Scharff.Domain.Response.ExchangeRate.GetExchangeRateBroadCast;
using Scharff.Infrastructure.Http.Queries.PaymentVoucher.GetPaymentVoucherByExchangeRate;
using Scharff.Infrastructure.PostgreSQL.Commands.ExchangeRate.UpdateExchangeRate;
using Scharff.Infrastructure.PostgreSQL.Queries.ExchangeRate.GetExchangeRateBroadCast;

namespace Scharff.UnitTest.Commands.ExchangeRate.UpdateExchangeRate
{
    public static class UpdateExchangeRateMock
    {
        public static Mock<IUpdateExchangeRateCommand> UpdateExchangeRate(int expectedRegisterId)
        {
            var mock = new Mock<IUpdateExchangeRateCommand>();

            mock.Setup(x => x.UpdateExchangeRate(It.IsAny<ExchangeRateModel>()))
                .ReturnsAsync(expectedRegisterId);

            return mock;
        }
        public static Mock<IGetPaymentVoucherByExchangeRateQuery> GetExchangeRateBroadCas(string query)
        {
            var expectedPaymentVoucher = 1;
            var mock = new Mock<IGetPaymentVoucherByExchangeRateQuery>();

            mock.Setup(x => x.GetPaymentVoucherByExchangeRate(query))
                .ReturnsAsync(expectedPaymentVoucher);

            return mock;
        }
    }
}
