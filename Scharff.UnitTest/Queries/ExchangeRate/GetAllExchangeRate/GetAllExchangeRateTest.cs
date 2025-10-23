using Scharff.Application.Queries.ExchangeRate.GetAllExchangeRate;
using Scharff.Domain.Response.ExchangeRate.GetAllExchangeRate;
using Scharff.Domain.Response.Utils;

namespace Scharff.UnitTest.Queries.ExchangeRate.GetAllExchangeRate
{
    public class GetAllExchangeRateTest
    {
        [Fact]
        public async Task GetAllClients_OkResult()
        {
            // Arrange
            var query = new GetAllExchangeRateQuery
            {
                pageNumber = 1,
                pageSize = 10
            };

            var mock = GetAllExchangeRateMock.GetAllClients_OkResult(query);
            var handler = new GetAllExchangeRateHandler(mock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PaginatedResponse<ResponseGetAllExchangeRate>>(result);
        }
    }
}
