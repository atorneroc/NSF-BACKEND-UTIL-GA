using Moq;
using Scharff.Domain.Response.BillingCourt;
using Scharff.Infrastructure.PostgreSQL.Queries.BillingCourt.GetProductByIdBusinessUnitIdCompany;

namespace Scharff.UnitTest.Queries.BillingCourt.GetProductByIdBusinessUnitIdCompany
{
    public class GetProductByIdBusinessUnitIdCompanyMock
    {
        public static Mock<IGetProductByIdBusinessUnitIdCompanyQuery> GetProductByIdBusinessUnitIdCompany_OkResult(int idCompany, int idBusinessUnit)
        {
            ResponseGetProductByIdBusinessUnitIdCompany expectedResponse = new()
            {
                Id_Producto = 4,
                Descripcion_Producto = "Callao 1"
            };

            var mock = new Mock<IGetProductByIdBusinessUnitIdCompanyQuery>();

            mock.Setup(x => x.GetProductByIdBusinessUnitIdCompany(idCompany, idBusinessUnit))
    .ReturnsAsync(new List<ResponseGetProductByIdBusinessUnitIdCompany> {expectedResponse});

            return mock;
        }

        public static Mock<IGetProductByIdBusinessUnitIdCompanyQuery> GetProductByIdBusinessUnitIdCompany_WhenIdDoesNotExist(int idCompany, int idBusinessUnit)
        {
            List<ResponseGetProductByIdBusinessUnitIdCompany>? expectedResponse = new();

            var mock = new Mock<IGetProductByIdBusinessUnitIdCompanyQuery>();

            mock.Setup(x => x.GetProductByIdBusinessUnitIdCompany(idCompany, idBusinessUnit))
                .ReturnsAsync(expectedResponse);

            return mock;
        }
    }
}
