using Scharff.Application.Queries.Ubigeo.CheckUbigeoByCode;
using Scharff.Domain.Response.Ubigeo.CheckUbigeoByCode;

namespace Scharff.UnitTest.Queries.Ubigeo.CheckUbigeoByCode
{
    public class CheckUbigeoByCodeTest
    {
        [Fact]
        public async Task CheckUbigeoByCode_OkResult()
        {
            // Arrange
            var query = new CheckUbigeoByCodeQuery
            {
                Ubigeo_code = "110108"
            };

            var mock = CheckUbigeoByCodeMock.GetCheckUbigeoByCode_OkResult();
            var handler = new CheckUbigeoByCodeHandler(mock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ResponseCheckUbigeoByCode>>(result);
        }
    }
}
