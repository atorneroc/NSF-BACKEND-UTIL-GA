using Scharff.Application.Queries.ExchangeRate.GetExchangeRateById;
using Scharff.Domain.Response.ExchangeRate.GetExchangeRateById;
using Scharff.Domain.Utils.Exceptions;

namespace Scharff.UnitTest.Queries.ExchangeRate.GetExchangeRateById
{
    public class GetExchangeRateByIdTest
    {
        [Fact]
        public async Task GetExchangeRateById_WhenIdDoesNotExist()
        {
            //Arrange
            int id = 99999999;
            GetExchangeRateByIdQuery query = new() { id = id };

            var mock = GetExchangeRateByIdMock.GetExchangeRateById_WhenIdDoesNotExist(id);
            var handler = new GetExchangeRateByIdHandler(mock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                var result = await handler.Handle(query, CancellationToken.None);
            });
        }

        [Fact]
        public async Task GetExchangeRateById_OkResult()
        {
            //Arrange
            int id = 1;
            var query = new GetExchangeRateByIdQuery
            {
                id = id
            };

            var mock = GetExchangeRateByIdMock.GetExchangeRateById_OkResult(id);
            var handler = new GetExchangeRateByIdHandler(mock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ResponseGetExchangeRateById>(result);
        }
    }
}
