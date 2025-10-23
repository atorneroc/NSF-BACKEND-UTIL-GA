using MediatR;
using Scharff.Domain.Response.Parameter.ValidateConfiguredServiceFree;

namespace Scharff.Application.Queries.Parameter.ValidateConfiguredServiceFree
{
    public class ValidateConfiguredServiceFreeQuery : IRequest<List<ConfiguredServiceFreeResponse>>
    {
        public ValidateConfiguredServiceFree[]? Queries { get; set; }
    }

    public class ValidateConfiguredServiceFree
    {
        public int IdOrganizationalServiceStructure { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
    }
}