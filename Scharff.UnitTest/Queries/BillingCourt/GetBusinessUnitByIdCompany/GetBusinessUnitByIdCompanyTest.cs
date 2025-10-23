using Scharff.Application.Queries.BillingCourt.GetBusinessUnitByIdCompany;
using Scharff.Domain.Response.BillingCourt;
using Scharff.Domain.Utils.Exceptions;

namespace Scharff.UnitTest.Queries.BillingCourt.GetBusinessUnitByIdCompany
{
    public class GetBusinessUnitByIdCompanyTest
    {
        [Fact]
        public async Task GetBusinessUnitByIdCompany_WhenIdDoesNotExist()
        { 
            GetBusinessUnitByIdCompanyQuery query = new() {Id_Company=2 };

            int idCompany = 2;

            var mock = GetBusinessUnitByIdCompanyMock.GetBusinessUnitByIdCompany_WhenIdDoesNotExist(idCompany);
            var handler = new GetBusinessUnitByIdCompanyHandler(mock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                var result = await handler.Handle(query, CancellationToken.None);
            });
        }

        [Fact]
        public async Task GetBusinessUnitByIdCompany_OkResult()
        {
            GetBusinessUnitByIdCompanyQuery query = new() {Id_Company= 2 };

            int idCompany = 2;

            var mock = GetBusinessUnitByIdCompanyMock.GetBusinessUnitByIdCompany_OkResult(idCompany);
            var handler = new GetBusinessUnitByIdCompanyHandler(mock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ResponseGetBusinessUnitByIdCompany>>(result);
        }
    }
}
