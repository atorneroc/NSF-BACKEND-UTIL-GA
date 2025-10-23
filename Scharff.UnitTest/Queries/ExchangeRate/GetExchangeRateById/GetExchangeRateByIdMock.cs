using Moq;
using Scharff.Domain.Response.ExchangeRate.GetExchangeRateById;
using Scharff.Infrastructure.PostgreSQL.Queries.ExchangeRate.GetExchangeRateById;

namespace Scharff.UnitTest.Queries.ExchangeRate.GetExchangeRateById
{
    public static class GetExchangeRateByIdMock
    {
        public static Mock<IGetExchangeRateByIdQuery> GetExchangeRateById_OkResult(int idExchangeRate)
        {
            ResponseGetExchangeRateById expectedResponse = new()
            {
                id = 1,
                date_change = DateTime.Now,
                bank_purchase = 1,
                bank_sale = 1,
            };

            var mock = new Mock<IGetExchangeRateByIdQuery>();

            mock.Setup(x => x.GetExchangeRateById(idExchangeRate))
                .ReturnsAsync(expectedResponse);

            return mock;
        }

        public static Mock<IGetExchangeRateByIdQuery> GetExchangeRateById_WhenIdDoesNotExist(int idExchangeRate)
        {
            ResponseGetExchangeRateById? response = null; 
            var mock = new Mock<IGetExchangeRateByIdQuery>();
            mock.Setup(x => x.GetExchangeRateById(idExchangeRate))
                .ReturnsAsync(response);
            return mock;
        }
    }
}
