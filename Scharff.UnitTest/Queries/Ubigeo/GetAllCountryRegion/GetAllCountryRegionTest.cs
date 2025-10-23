using Scharff.Application.Queries.Parameter.GetAllCountryRegion;
using Scharff.Domain.Response.Ubigeo.GetAllCountryRegion;
using Scharff.Domain.Response.Ubigeo.GetUbigeoByCode;

namespace Scharff.UnitTest.Queries.Client.GetAllCountryRegion
{
    public class GetAllCountryRegionTest
    {
        [Fact]
        public async Task GetAllCountryRegion_OkResult()
        {
            // Arrange
            var query = new GetAllCountryRegionQuery
            {
                
            };

            var mock = GetAllCountryRegionMock.GetAllCountryRegion_OkResult();
            var handler = new GetAllCountryRegionHandler(mock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ResponseGetAllCountryRegion>>(result);
        }
    }
}
