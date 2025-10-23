using Scharff.Application.Commands.ExchangeRate.UpdateExchangeRate;
using Scharff.UnitTest.Commands.ExchangeRate.UpdateExchangeRate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.UnitTest.Commands.ExchangeRate.UpdateExchangeRate
{
    public class UpdateExchangeRateTest
    {
        [Fact]
        public async Task UpdateExchangeRate_WithValidRequest()
        {
            // Arrange
            var command = new UpdateExchangeRateCommand
            {   
                id = 1,
                bank_sale = 3,
                bank_purchase = 3,
                user = "test"
            };
            var validator = new UpdateExchangeRateCommandValidator();

            //Act
            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task UpdateExchangeRate_WithInvalidRequest()
        {
            // Arrange
            var command = new UpdateExchangeRateCommand
            {

            };

            var validator = new UpdateExchangeRateCommandValidator();

            //Act
            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task UpdateExchangeRate_OkResult()
        {
            // Arrange
            var command = new UpdateExchangeRateCommand
            {
                id = 1,
                bank_sale = 3,
                bank_purchase = 3,
                user = "test"
            };
            var expectedUpdateId = 1;
            var datenow = DateTime.Now.ToShortDateString();
            var UpdateExchangeRateRepositoryMock = UpdateExchangeRateMock.UpdateExchangeRate(expectedUpdateId);
            var getExchangeRateBroadCas = UpdateExchangeRateMock.GetExchangeRateBroadCas(datenow);
            var handler = new UpdateExchangeRateCommandHandler(UpdateExchangeRateRepositoryMock.Object, getExchangeRateBroadCas.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedUpdateId, result);
        }
    }
}
