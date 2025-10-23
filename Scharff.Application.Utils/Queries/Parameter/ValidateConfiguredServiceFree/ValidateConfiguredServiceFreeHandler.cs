using MediatR;
using Scharff.Domain.Response.Parameter.ValidateConfiguredServiceFree;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.ValidateConfiguredServiceFree;
using System.ComponentModel.DataAnnotations;

namespace Scharff.Application.Queries.Parameter.ValidateConfiguredServiceFree
{
    public class ValidateConfiguredServiceFreeHandler : IRequestHandler<ValidateConfiguredServiceFreeQuery, List<ConfiguredServiceFreeResponse>>
    {
        private readonly IValidateConfiguredServiceFreeQuery _validateConfiguredServiceFreeQuery;
        public ValidateConfiguredServiceFreeHandler(IValidateConfiguredServiceFreeQuery validateConfiguredServiceFreeQuery)
        {
            _validateConfiguredServiceFreeQuery = validateConfiguredServiceFreeQuery;
        }
        public async Task<List<ConfiguredServiceFreeResponse>> Handle(ValidateConfiguredServiceFreeQuery request, CancellationToken cancellationToken)
        {
            var result = new List<ConfiguredServiceFreeResponse>();

            foreach (var service in request.Queries!)
            {

                var configuredService = await _validateConfiguredServiceFreeQuery.GetFreeConfigurationByServiceIdAsync(service.IdOrganizationalServiceStructure);

                if (configuredService == null || configuredService.Id_Free_Type_Affectation <= 0)
                {
                    throw new ValidationException(
                     $"No existe configuración de afectación gratuita para el servicio: " +
                     $"{service.IdOrganizationalServiceStructure} - {service.Description} - {service.Code}"
                    );
                }

                result.Add(configuredService);
            }
            return result;
        }
    }
}
