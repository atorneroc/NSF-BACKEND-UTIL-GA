using Azure;
using Moq;
using Scharff.Domain.Response.Ubigeo.GetUbigeoByCode;
using Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.GetUbigeoByCodeQuery;

namespace Scharff.UnitTest.Queries.Client.GetUbigeoByCode
{
    public class GetUbigeoByCodeMock
    {
        public static Mock<IGetUbigeoByCodeQuery> GetUbigeoByCode_OkResult(List<string> Ubigeo_Code)
        {
            List<ResponseGetUbigeoByCode> expectedResponse = new()
            {
               new ResponseGetUbigeoByCode { Ubigeo_Code = "15",Description="LIMA" , id =1, Postal_Code="" },
               new ResponseGetUbigeoByCode { Ubigeo_Code = "16",Description="CALLAO" , id =1, Postal_Code="" },
               new ResponseGetUbigeoByCode { Ubigeo_Code = "17",Description="LURIN" , id =1, Postal_Code="" }
            };

            var mock = new Mock<IGetUbigeoByCodeQuery>();

            mock.Setup(x => x.GetUbigeoByCode(Ubigeo_Code))
               .ReturnsAsync(expectedResponse);

            return mock;
        }

        public static Mock<IGetUbigeoByCodeQuery> GetUbigeoByCode_WhenIdDoesNotExist(List<string> Ubigeo_Code)
        {
            List<ResponseGetUbigeoByCode> expectedResponse = new();

            var mock = new Mock<IGetUbigeoByCodeQuery>();

            mock.Setup(x => x.GetUbigeoByCode(Ubigeo_Code))
               .ReturnsAsync(expectedResponse);

            return mock;
        }
    }
}
