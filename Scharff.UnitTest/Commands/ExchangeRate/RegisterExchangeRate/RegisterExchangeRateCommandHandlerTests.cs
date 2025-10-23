using Microsoft.AspNetCore.Routing;
using Moq;
using Scharff.Application.Commands.ExchangeRate.RegisterExchangeRate;
using Scharff.Domain.Entities;
using Scharff.Domain.Response.ExchangeRate.GetExchangeRateBroadCast;
using Scharff.Domain.Utils.Exceptions;
using Scharff.Infrastructure.PostgreSQL.Commands.ExchangeRate.RegisterExchangeRate;
using Scharff.Infrastructure.PostgreSQL.Queries.ExchangeRate.GetExchangeRateBroadCast;

namespace Scharff.UnitTest.Commands.ExchangeRate.RegisterExchangeRate
{
    public class RegisterExchangeRateCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidRequest_ReturnsServiceOrderId()
        {
            // Arrange
            var request = new Application.Commands.ExchangeRate.RegisterExchangeRate.RegisterExchangeRateCommand
            {
                change_date = DateTime.Now,
                bank_purchase = 1.0m,
                bank_sale = 1.5m,
                user = "testUser"
            };
            var expectedResult = 123; // Replace with expected service order ID

            var getExchangeRateBroadCastMock = new Mock<IGetExchangeRateBroadCast>();
            getExchangeRateBroadCastMock
                .Setup(m => m.GetExchangeRateByBroadCast(request.change_date.Date))
                .ReturnsAsync((ResponseGetExchangeRateBroadCast)null); // Simulate no existing exchange rate for the specified date

            var registerExchangeRateCommandMock = new Mock<IRegisterExchangeRateCommand>();
            registerExchangeRateCommandMock
                .Setup(m => m.RegisterExchangeRate(It.IsAny<ExchangeRateModel>()))
                .ReturnsAsync(expectedResult);

            var handler = new RegisterExchangeRateCommandHandler(registerExchangeRateCommandMock.Object, getExchangeRateBroadCastMock.Object);
            var cancellationToken = CancellationToken.None;

            // Act
            var result = await handler.Handle(request, cancellationToken);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task Handle_ExistingExchangeRate_ThrowsBadRequestException()
        {
            // Arrange
            var request = new Application.Commands.ExchangeRate.RegisterExchangeRate.RegisterExchangeRateCommand
            {
                change_date = DateTime.Now,
                bank_purchase = 1.0m,
                bank_sale = 1.5m,
                user = "testUser"
            };

            var existingExchangeRate = new ResponseGetExchangeRateBroadCast { id=1}; // Simulate an existing exchange rate

            var getExchangeRateBroadCastMock = new Mock<IGetExchangeRateBroadCast>();
            getExchangeRateBroadCastMock
                .Setup(m => m.GetExchangeRateByBroadCast(request.change_date.Date))
                .ReturnsAsync(existingExchangeRate);

            var registerExchangeRateCommandMock = new Mock<IRegisterExchangeRateCommand>();

            var handler = new RegisterExchangeRateCommandHandler(registerExchangeRateCommandMock.Object, getExchangeRateBroadCastMock.Object);
            var cancellationToken = CancellationToken.None;

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(request, cancellationToken));
        }

        // Add more tests as needed for other scenarios (e.g., invalid requests, exceptions, etc.)


    }
}
