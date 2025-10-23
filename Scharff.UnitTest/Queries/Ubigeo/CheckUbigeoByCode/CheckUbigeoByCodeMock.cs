using Moq;
using Scharff.Domain.Response.Ubigeo.CheckUbigeoByCode;
using Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.CheckUbigeoByCode;

namespace Scharff.UnitTest.Queries.Ubigeo.CheckUbigeoByCode
{
    public static class CheckUbigeoByCodeMock
    {
        public static Mock<ICheckUbigeoByCodeQuery> GetCheckUbigeoByCode_OkResult()
        {
            List<ResponseCheckUbigeoByCode> response = new()
            {
                new ResponseCheckUbigeoByCode
                {
                    Id = 1,
                    Parent_id = 0,
                    Description = "PERU",
                    UbigeoCode = "",
                    Level = "1",
                    Label = "PAIS"
                },
                new ResponseCheckUbigeoByCode
                {
                    Id = 12,
                    Parent_id = 1,
                    Description = "ICA",
                    UbigeoCode = "11",
                    Level = "2",
                    Label = "DEPARTAMENTO"
                },
                new ResponseCheckUbigeoByCode
                {
                    Id = 125,
                    Parent_id = 12,
                    Description = "ICA",
                    UbigeoCode = "1101",
                    Level = "3",
                    Label = "PROVINCIA"
                },
                new ResponseCheckUbigeoByCode
                {
                    Id = 1222,
                    Parent_id = 125,
                    Description = "SALAS",
                    UbigeoCode = "110108",
                    Level = "4",
                    Label = "DISTRITO"
                }
            };

            var mock = new Mock<ICheckUbigeoByCodeQuery>();

            mock
                .Setup(x => x.CheckUbigeoByCode(It.IsAny<string>()))
                .ReturnsAsync(response);

            return mock;
        }
    }
}
