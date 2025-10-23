using Moq;
using Scharff.Application.Queries.ExchangeRate.GetAllExchangeRate;
using Scharff.Domain.Response.ExchangeRate.GetAllExchangeRate;
using Scharff.Domain.Response.Utils;
using Scharff.Infrastructure.PostgreSQL.Queries.ExchangeRate.GetAllExchangeRate;

namespace Scharff.UnitTest.Queries.ExchangeRate.GetAllExchangeRate
{
    
    public class GetAllExchangeRateHandlerTests
    {
        [Fact]
        public async Task Handle_ValidRequest_ReturnsResponse()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 10;
            var dateChange = DateTime.Now;
            var expectedResult = new PaginatedResponse<ResponseGetAllExchangeRate>(); // Set your expected result here

            var getAllExchangeRateQueryMock = new Mock<IGetAllExchangeRateQuery>();
            getAllExchangeRateQueryMock
                .Setup(m => m.GetAllExchangeRate(pageNumber, pageSize, dateChange))
                .ReturnsAsync(expectedResult);

            var handler = new GetAllExchangeRateHandler(getAllExchangeRateQueryMock.Object);
            var request = new Application.Queries.ExchangeRate.GetAllExchangeRate.GetAllExchangeRateQuery { pageNumber= pageNumber,pageSize= pageSize,date_change= dateChange };
            var cancellationToken = CancellationToken.None;

            // Act
            var result = await handler.Handle(request, cancellationToken);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
