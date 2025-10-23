using Moq;
using Scharff.Application.Queries.Parameter.CheckExternalExistenceUser;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.CheckExternalExistenceUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.UnitTest.Queries.Parameter.CheckExternalExistenceUser
{
    public class CheckExternalExistenceUserHandlerTests
    {
        private readonly Mock<ICheckExternalExistenceUserQuery> _mockCheckQuery;
        private readonly CheckExternalExistenceUserHandler _handler;

        public CheckExternalExistenceUserHandlerTests()
        {
            _mockCheckQuery = new Mock<ICheckExternalExistenceUserQuery>();
            _handler = new CheckExternalExistenceUserHandler(_mockCheckQuery.Object);
        }
        [Fact]
        public async Task Handle_ShouldReturnExpectedResult_WhenQueryIsSuccessful()
        {
            // Arrange
            var query = new Application.Queries.Parameter.CheckExternalExistenceUser.CheckExternalExistenceUserQuery { User_Email = "test@example.com" };
            var expectedResult = "User Exists";

            _mockCheckQuery.Setup(q => q.CheckExternalExistenceUser(It.IsAny<string>()))
                           .ReturnsAsync(expectedResult);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(expectedResult, result);
            _mockCheckQuery.Verify(q => q.CheckExternalExistenceUser(query.User_Email), Times.Once);
        }
        [Fact]
        public async Task Handle_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var query = new Application.Queries.Parameter.CheckExternalExistenceUser.CheckExternalExistenceUserQuery { User_Email = "nonexistent@example.com" };
            string expectedResult = null;

            _mockCheckQuery.Setup(q => q.CheckExternalExistenceUser(It.IsAny<string>()))
                           .ReturnsAsync(expectedResult);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
            _mockCheckQuery.Verify(q => q.CheckExternalExistenceUser(query.User_Email), Times.Once);
        }
    }

}
