using Moq;
using Scharff.Domain.Response.Ubigeo.GetByIdCountryRegion;
using Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.GetByIdCountryRegion;

namespace Scharff.UnitTest.Queries.Ubigeo.GetByIdCountryRegion
{
    public class GetByIdCountryRegionMock
    {
        public static Mock<IGetByIdCountryRegionQuery> GetByIdCountryRegion_OkResult()
        {
            List<ResponseGetByIdCountryRegion> response = new()
            {
                new ResponseGetByIdCountryRegion {
                    id = 1, Description = "Josue Almanza" ,Sap_Country_Code = "" , Sap_Region_Code = "", Ubigeo_Code    =""},
                new ResponseGetByIdCountryRegion {
                    id = 2, Description = "Noelia De la Cruz",Sap_Country_Code = "" , Sap_Region_Code = "", Ubigeo_Code    =""}
            };
            var mock = new Mock<IGetByIdCountryRegionQuery>();

            mock
                .Setup(x => x.GetByIdCountryRegion(16))
                .ReturnsAsync(response);

            
            return mock;
        }
    }
}
