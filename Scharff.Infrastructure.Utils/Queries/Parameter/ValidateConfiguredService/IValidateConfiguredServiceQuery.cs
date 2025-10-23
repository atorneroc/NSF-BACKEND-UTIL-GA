using Scharff.Domain.Response.Parameter.ValidateConfiguredService;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.ValidateConfiguredService
{
    public interface IValidateConfiguredServiceQuery
    {
        Task<int> GetOrganizationStructure(int? idCompany, int? idBranch, int? idBusinessUnit, string serviceTypeCode);
        Task<List<ConfiguredServiceResponse>> GetOrganizationStructureService(int idOrganizationStructure);
    }
}