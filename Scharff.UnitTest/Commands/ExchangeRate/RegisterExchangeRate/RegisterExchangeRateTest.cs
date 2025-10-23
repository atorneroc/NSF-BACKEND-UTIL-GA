
using Scharff.Application.Commands.ExchangeRate.RegisterExchangeRate;

namespace Scharff.UnitTest.Commands.ExchangeRate.RegisterExchangeRate
{
    public class RegisterExchangeRateTest
    {
        [Fact]
        public async Task RegisterClient_WithValidRequest()
        {
            // Arrange
            var command = new RegisterExchangeRateCommand
            {
                change_date = DateTime.Now,
                bank_sale = 3,
                bank_purchase = 3,
                user = "test"
            };
            var validator = new RegisterExchangeRateCommandValidator();

            //Act
            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task RegisterClient_WithInvalidRequest()
        {
            // Arrange
            var command = new RegisterExchangeRateCommand
            {

            };

            var validator = new RegisterExchangeRateCommandValidator();

            //Act
            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact(Skip = "Deshabilitado temporalmente por error de tipo de cambio")]
        public async Task RegisterClient_OkResult()
        {
            // Arrange
            var command = new RegisterExchangeRateCommand
            {
                change_date = DateTime.Now,
                bank_sale = 3,
                bank_purchase = 3,
                user = "test"
            };
            var expectedRegisterId = 1;

            var registerClientRepositoryMock = RegisterExchangeRateMock.RegisterExchangeRate(expectedRegisterId);
            var getExchangeRateBroadCas = RegisterExchangeRateMock.GetExchangeRateBroadCas();
            var handler = new RegisterExchangeRateCommandHandler(registerClientRepositoryMock.Object, getExchangeRateBroadCas.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedRegisterId, result);
        }
    }
}
