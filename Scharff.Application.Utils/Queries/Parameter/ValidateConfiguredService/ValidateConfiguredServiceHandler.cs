using AutoMapper;
using MediatR;
using Scharff.Domain.Entities.External;
using Scharff.Domain.Entities;
using Scharff.Domain.Response.Parameter.ValidateConfiguredService;
using Scharff.Infrastructure.PostgreSQL.Constants;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.ValidateConfiguredService;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.Generic;

namespace Scharff.Application.Queries.Parameter.ValidateConfiguredService
{
    public class ValidateConfiguredServiceHandler : IRequestHandler<ValidateConfiguredServiceQuery, List<ConfiguredServiceResponse>>
    {
        private readonly IValidateConfiguredServiceQuery _validateConfiguredServiceQuery;
        private readonly IMapper _mapper;
        private readonly IGenericQuery _genericQuery;

        public ValidateConfiguredServiceHandler(IValidateConfiguredServiceQuery validateConfiguredServiceQuery, IGenericQuery genericQuery, IMapper mapper)
        {
            _validateConfiguredServiceQuery = validateConfiguredServiceQuery;
            _genericQuery = genericQuery;
            _mapper = mapper;

        }

        public async Task<List<ConfiguredServiceResponse>> Handle(ValidateConfiguredServiceQuery request, CancellationToken cancellationToken)
        {
            var valuesDictionary = await CheckFiltersInvalid(request);
            var filter = Map(valuesDictionary);

            var idOrganizationStructure = await _validateConfiguredServiceQuery.GetOrganizationStructure(filter.Company.id, filter.Branch.id, filter.BusinessUnit.id, request.serviceTypeCode);

            if (idOrganizationStructure == 0)
            {
                return new List<ConfiguredServiceResponse>();
            }

            var organizationStructureServiceList = await _validateConfiguredServiceQuery.GetOrganizationStructureService(idOrganizationStructure);
            
            return organizationStructureServiceList;
        }

        private async Task<Dictionary<string, object?>> CheckFiltersInvalid(ValidateConfiguredServiceQuery externalRequest)
        {
            var results = await CheckIndividualFiltersAsync(externalRequest);
            return results;
        }
        public async Task<Dictionary<string, object?>> CheckIndividualFiltersAsync(ValidateConfiguredServiceQuery externalRequest)
        {
            var results = new Dictionary<string, object?>();

            results[DatabaseConstants.COMPANY_TABLE] = await GetCompanyDataAsync(externalRequest.companyDocNumber);
            results[DatabaseConstants.BRANCH_TABLE] = await GetBranchDataAsync(externalRequest.branchCode);
            results[DatabaseConstants.BUSINESS_UNIT_TABLE] = await GetBusinessUnitDataAsync(externalRequest.businessUnitCode);

            return results;
        }
        private async Task<Empresa?> GetCompanyDataAsync(string documentNumber)
        {
            var columns = new List<string> { "id", "razon_social" };
            return await _genericQuery.GetColumnAsync<Empresa>(
                DatabaseConstants.SCHEMA_NSF,
                DatabaseConstants.COMPANY_TABLE,
                columns,
                new { numero_documento_upper = documentNumber });
        }
        private async Task<Sucursal?> GetBranchDataAsync(string branchCodeIntOF)
        {
            var columns = new List<string> { "id" };
            return await _genericQuery.GetColumnAsync<Sucursal>(
                DatabaseConstants.SCHEMA_NSF,
                DatabaseConstants.BRANCH_TABLE,
                columns,
                new { codigo_integracion_of_upper = branchCodeIntOF });
        }
        private async Task<UnidadNegocio?> GetBusinessUnitDataAsync(string businessCodeIntOF)
        {
            var columns = new List<string> { "id" };
            return await _genericQuery.GetColumnAsync<UnidadNegocio>(
                DatabaseConstants.SCHEMA_NSF,
                DatabaseConstants.BUSINESS_UNIT_TABLE,
                columns,
                new { codigo_integracion_of_upper = businessCodeIntOF });
        }
        public Filter Map(Dictionary<string, object?> valuesDictionary)
        {
            var filter = new Filter
            {
                Company = GetValue<Empresa>(valuesDictionary, "empresa") ?? new Empresa { id = 0 },
                Branch = GetValue<Sucursal>(valuesDictionary, "sucursal") ?? new Sucursal { id = 0 },
                BusinessUnit = GetValue<UnidadNegocio>(valuesDictionary, "unidad_negocio") ?? new UnidadNegocio { id = 0 },
                Service = GetValue<Servicio>(valuesDictionary, "servicio") ?? new Servicio { id = 0 }
            };

            return filter;
        }
        private T? GetValue<T>(Dictionary<string, object?> dictionary, string key) where T : class
        {
            if (dictionary.TryGetValue(key, out var value) && value is T typedValue)
            {
                return typedValue;
            }
            return null;
        }
    }
}
