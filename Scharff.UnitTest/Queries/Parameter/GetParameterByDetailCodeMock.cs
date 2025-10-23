using Moq;
using Scharff.Domain.Response.Parameter.GetParameterByCodeDetail;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetParameterByDetailCode;

namespace Scharff.UnitTest.Queries.Parameter
{
    public  class GetParameterByDetailCodeMock
    {
        public static Mock<IGetParameterByDetailCodeQuery> GetParameterByDetailCode_OkResult(List<string> Lst_Codes)
        {
            List<ResponseGetParameterByDetailCode> expectedResponse = new()
            {
                new ResponseGetParameterByDetailCode { Id = 82, Name = "Cxc Clientes Corporativos", Description = "Cxc Clientes Corporativos", Detail_Code = "1", General_Code = "5", General_Description = "Tipo Segmentación" },
                new ResponseGetParameterByDetailCode { Id = 53, Name = "Corporativo", Description = "Corporativo", Detail_Code = "CTGCORP", General_Code = "CTGCLIENT", General_Description = "Categoría Cliente" }
            };

            var mock = new Mock<IGetParameterByDetailCodeQuery>();
            mock.Setup(x => x.GetParameterByCodeDetail(Lst_Codes)).ReturnsAsync(expectedResponse);

            return mock;
        }
    }
}