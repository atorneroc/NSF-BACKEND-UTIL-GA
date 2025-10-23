using Scharff.Application.Queries.Ubigeo.GetUbigeoByCode;
using Scharff.Domain.Response.Ubigeo.GetUbigeoByCode;
using Scharff.Domain.Utils.Exceptions;

namespace Scharff.UnitTest.Queries.Client.GetUbigeoByCode
{
    public class GetUbigeoByCodeTest
    {
        [Fact]
        public async Task GetUbigeoByCode_WhenIdDoesNotExist()
        {
            //Arrange
            List<string> UbigeoCode = new List<string> { "15" };
            GetUbigeoByCodeQuery query = new() { ubigeoCode = { "15" } };

            var mock = GetUbigeoByCodeMock.GetUbigeoByCode_WhenIdDoesNotExist(UbigeoCode);
            var handler = new GetUbigeoByCodeHandler(mock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                var result = await handler.Handle(query, CancellationToken.None);
            });
        }

        [Fact]
        public async Task GetUbigeoByCode_OkResult()
        {
            //Arrange
            List<string> UbigeoCode = new List<string> { "15" };
            var query = new GetUbigeoByCodeQuery
            {
                ubigeoCode = UbigeoCode
            };

            var mock = GetUbigeoByCodeMock.GetUbigeoByCode_OkResult(UbigeoCode);
            var handler = new GetUbigeoByCodeHandler(mock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ResponseGetUbigeoByCode>>(result);
        }
    }
}
