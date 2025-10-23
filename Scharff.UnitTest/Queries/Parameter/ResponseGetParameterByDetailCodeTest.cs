using Scharff.Application.Queries.Parameter.GetParameterByDetailCode;

namespace Scharff.UnitTest.Queries.Parameter
{
    public  class ResponseGetParameterByDetailCodeTest
    {
       
        [Fact]
        public async Task GetParameterByDetailCode_OkResult()
        {

            // Arrange
            var request = new GetParameterByDetailCodeQuery { Lst_Codes = new List<string> { "5-1", "CTGCLIENT-CTGCORP" } };
            var mock = GetParameterByDetailCodeMock.GetParameterByDetailCode_OkResult(request.Lst_Codes);
            var handler = new GetParameterByDetailCodeHandler(mock.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count);
        }

    }
}