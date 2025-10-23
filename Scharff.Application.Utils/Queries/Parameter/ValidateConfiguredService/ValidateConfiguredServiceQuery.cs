using MediatR;
using Scharff.Domain.Response.Parameter.ValidateConfiguredService;

namespace Scharff.Application.Queries.Parameter.ValidateConfiguredService
{
    public class ValidateConfiguredServiceQuery : IRequest<List<ConfiguredServiceResponse>>
    {
        public string companyDocNumber { get; set; } = string.Empty;
        public string branchCode { get; set; } = string.Empty;
        public string businessUnitCode { get; set; } = string.Empty;
        public string serviceTypeCode { get; set; } = string.Empty;
    }
}