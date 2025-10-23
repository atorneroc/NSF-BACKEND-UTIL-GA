using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Xunit;
using Scharff.Application.Queries.Parameter.ValidateExternalBaseStructure;
using Scharff.Domain.Entities;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.ValidateExternalBaseStructure;
using Scharff.Domain.Response.Parameter.ValidateExternalBaseStructure;

namespace Scharff.UnitTest.Queries.Parameter.ValidateExternalBaseStructure
{
    public class ValidateExternalBaseStructureHandlerTests
    {
        private readonly Mock<IValidateExternalBaseStructureQuery> _mockValidateQuery;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ValidateExternalBaseStructureHandler _handler;

        public ValidateExternalBaseStructureHandlerTests()
        {
            _mockValidateQuery = new Mock<IValidateExternalBaseStructureQuery>();
            _mockMapper = new Mock<IMapper>();
            _handler = new ValidateExternalBaseStructureHandler(_mockValidateQuery.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnExpectedResult_WhenValidationIsSuccessful()
        {
            // Arrange
            var query = new Application.Queries.Parameter.ValidateExternalBaseStructure.ValidateExternalBaseStructureQuery
            {
                DocumentNumber = "123456",
                BranchCodeIntOF = "001",
                BusinessUnitCodeIntOF = "BU01",
                ProductCodeIntOF = "P001",
                ServiceCodeIntOF = "S001"
            };

            var externalBaseStructure = new ExternalBaseStructure
            {
                DocumentNumber = "123456",
                BranchCodeIntOF = "001",
                BusinessUnitCodeIntOF = "BU01",
                ProductCodeIntOF = "P001",
                ServiceCodeIntOF = "S001"
            };

            var expectedResponse = new BaseServiceResponse
            {
                BaseId = 1,
                BaseServiceId = 2

            };

            _mockMapper.Setup(m => m.Map<ExternalBaseStructure>(It.IsAny<Application.Queries.Parameter.ValidateExternalBaseStructure.ValidateExternalBaseStructureQuery>()))
                       .Returns(externalBaseStructure);

            _mockValidateQuery.Setup(v => v.ValidateExternalBaseStructure(It.IsAny<ExternalBaseStructure>()))
                              .ReturnsAsync(expectedResponse);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.BaseId, result.BaseId);
            Assert.Equal(expectedResponse.BaseServiceId, result.BaseServiceId);

            _mockMapper.Verify(m => m.Map<ExternalBaseStructure>(query), Times.Once);
            _mockValidateQuery.Verify(v => v.ValidateExternalBaseStructure(externalBaseStructure), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenMapperFails()
        {
            // Arrange
            var query = new Application.Queries.Parameter.ValidateExternalBaseStructure.ValidateExternalBaseStructureQuery
            {
                DocumentNumber = "123456",
                BranchCodeIntOF = "001",
                BusinessUnitCodeIntOF = "BU01",
                ProductCodeIntOF = "P001",
                ServiceCodeIntOF = "S001"
            };

            _mockMapper.Setup(m => m.Map<ExternalBaseStructure>(It.IsAny<Application.Queries.Parameter.ValidateExternalBaseStructure.ValidateExternalBaseStructureQuery>()))
                       .Throws(new AutoMapperMappingException("Mapping failed"));

            // Act & Assert
            await Assert.ThrowsAsync<AutoMapperMappingException>(() => _handler.Handle(query, CancellationToken.None));
        }
    }
}