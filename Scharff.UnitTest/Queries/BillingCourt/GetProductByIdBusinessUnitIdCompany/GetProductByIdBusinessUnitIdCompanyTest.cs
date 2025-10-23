using Scharff.Application.Queries.BillingCourt.GetProductByIdBusinessUnitIdCompany;
using Scharff.Domain.Response.BillingCourt;
using Scharff.Domain.Utils.Exceptions;
using Scharff.UnitTest.Queries.BillingCourt.GetProductByIdBusinessUnitIdCompany;

namespace SScharff.UnitTest.Queries.BillingCourt.GetProductByIdBusinessUnitIdCompany
{
    public class GetProductByIdBusinessUnitIdCompanyTest
    {
        [Fact]
        public async Task GetProductByIdBusinessUnitIdCompany_WhenIdDoesNotExist()
        {
            GetProductByIdBusinessUnitIdCompanyQuery query = new()
            {
                Id_Business_Unit = 2,
                Id_Company = 2,
            };

            int idCompany = 2;
            int idBusinessUnit = 2;

            var mock = GetProductByIdBusinessUnitIdCompanyMock.GetProductByIdBusinessUnitIdCompany_WhenIdDoesNotExist(idCompany, idBusinessUnit);
            var handler = new GetProductByIdBusinessUnitIdCompanyHandler(mock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                var result = await handler.Handle(query, CancellationToken.None);
            });
        }

        [Fact]
        public async Task GetProductByIdBusinessUnitIdCompany_OkResult()
        {
            GetProductByIdBusinessUnitIdCompanyQuery query = new()
            {
                Id_Business_Unit = 2,
                Id_Company = 2,
            };

            int idCompany = 2;
            int idBusinessUnit = 2;

            var mock = GetProductByIdBusinessUnitIdCompanyMock.GetProductByIdBusinessUnitIdCompany_OkResult(idCompany, idBusinessUnit);
            var handler = new GetProductByIdBusinessUnitIdCompanyHandler(mock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ResponseGetProductByIdBusinessUnitIdCompany>>(result);
        }
    }
}
