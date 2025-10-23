using Scharff.Domain.Entities;
using Scharff.Domain.Response.Parameter.ValidateExternalBaseStructure;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.ValidateExternalBaseStructure
{
    public interface IValidateExternalBaseStructureQuery
    {
     Task<BaseServiceResponse>  ValidateExternalBaseStructure(ExternalBaseStructure externalRequest);
    }
}
