using Moq;
using Scharff.Domain.Response.Ubigeo.GetAllCountryRegion;
using Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.GetAllCountryRegion;

namespace Scharff.UnitTest.Queries.Client.GetAllCountryRegion
{
    public static class GetAllCountryRegionMock
    {
        public static Mock<IGetAllCountryRegionQuery> GetAllCountryRegion_OkResult()
        {
            List<ResponseGetAllCountryRegion> response = new()
            {
                new ResponseGetAllCountryRegion {
                    id = 1, Description = "Diego Rodas" ,Sap_Country_Code = "" , Sap_Region_Code = "", Ubigeo_Code    =""},
                new ResponseGetAllCountryRegion {
                    id = 2, Description = "Noelia De la Cruz",Sap_Country_Code = "" , Sap_Region_Code = "", Ubigeo_Code    =""}
            };
            var mock = new Mock<IGetAllCountryRegionQuery>();

            mock
                .Setup(x => x.GetAllCountryRegion(It.IsAny<string>()))
                .ReturnsAsync(response);


            return mock;
        }
    }
}
