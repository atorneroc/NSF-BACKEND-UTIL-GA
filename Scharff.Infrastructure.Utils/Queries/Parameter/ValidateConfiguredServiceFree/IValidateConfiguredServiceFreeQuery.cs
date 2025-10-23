using Scharff.Domain.Response.Parameter.ValidateConfiguredServiceFree;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.ValidateConfiguredServiceFree
{
    public interface IValidateConfiguredServiceFreeQuery
    {
        Task<ConfiguredServiceFreeResponse> GetFreeConfigurationByServiceIdAsync(int organizationalServiceStructureId);
    }
}