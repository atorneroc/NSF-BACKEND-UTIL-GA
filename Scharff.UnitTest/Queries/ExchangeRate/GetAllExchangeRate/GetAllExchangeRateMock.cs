using Moq;
using Scharff.Domain.Response.ExchangeRate.GetAllExchangeRate;
using Scharff.Domain.Response.Utils;
using Scharff.Infrastructure.PostgreSQL.Queries.ExchangeRate.GetAllExchangeRate;

namespace Scharff.UnitTest.Queries.ExchangeRate.GetAllExchangeRate
{
    public static class GetAllExchangeRateMock
    {
        public static Mock<IGetAllExchangeRateQuery> GetAllClients_OkResult(Application.Queries.ExchangeRate.GetAllExchangeRate.GetAllExchangeRateQuery query)
        {
            PaginatedResponse<ResponseGetAllExchangeRate> expectedResponse = new()
            {
                Total_rows = 2,
                Result = new List<ResponseGetAllExchangeRate>
                {
                    new ResponseGetAllExchangeRate { id = 1, date_change = DateTime.Now ,bank_purchase=1,bank_sale=1 },
                    new ResponseGetAllExchangeRate { id = 2, date_change = DateTime.Now ,bank_purchase=21,bank_sale=2 }
                }
            };

            var mock = new Mock<IGetAllExchangeRateQuery>();

            mock
                .Setup(x => x.GetAllExchangeRate(
                    query.pageNumber,
                    query.pageSize,
                    query.date_change))
                .ReturnsAsync(expectedResponse);

            return mock;
        }
    }
}
