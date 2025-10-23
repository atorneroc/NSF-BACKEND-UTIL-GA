using Moq;
using Scharff.Domain.Response.BillingCourt;
using Scharff.Infrastructure.PostgreSQL.Queries.BillingCourt.GetBusinessUnitByIdCompany;

namespace Scharff.UnitTest.Queries.BillingCourt.GetBusinessUnitByIdCompany
{
    public class GetBusinessUnitByIdCompanyMock
    {
        public static Mock<IGetBusinessUnitByIdCompanyQuery> GetBusinessUnitByIdCompany_OkResult(int idCompany)
        {
            ResponseGetBusinessUnitByIdCompany expectedResponse = new()
            {
                Id_Unidad_Negocio = 2,
                Descripcion_Unidad_Negocio = "ALMACEN"
            };

            var mock = new Mock<IGetBusinessUnitByIdCompanyQuery>();

            mock.Setup(x => x.GetBusinessUnitByIdCompany(idCompany))
    .ReturnsAsync(new List<ResponseGetBusinessUnitByIdCompany> {expectedResponse});

            return mock;
        }

        public static Mock<IGetBusinessUnitByIdCompanyQuery> GetBusinessUnitByIdCompany_WhenIdDoesNotExist(int idCompany)
        {
            List<ResponseGetBusinessUnitByIdCompany>? response = new();

            var mock = new Mock<IGetBusinessUnitByIdCompanyQuery>();

            mock.Setup(x => x.GetBusinessUnitByIdCompany(idCompany))
               .ReturnsAsync(response);

            return mock;
        }
    }
}
