using Scharff.Domain.Entities;
using Scharff.Domain.Entities.External;
using Scharff.Domain.Response.Parameter.ValidateExternalBaseStructure;
using Scharff.Infrastructure.PostgreSQL.Constants;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.ValidateExternalBaseStructure
{
    public class ValidateExternalBaseStructureQuery : IValidateExternalBaseStructureQuery
    {
        private readonly IDbConnection _connection;
        private readonly IGenericQuery _genericQuery;

        public ValidateExternalBaseStructureQuery(IDbConnection connection,IGenericQuery genericQuery)
        {
            _connection = connection;
            _genericQuery = genericQuery;
        }

        public async Task<int?> GetMatchingRecordBase(Filter filter, ExternalBaseStructure externalRequest)
        {
            var recordIdActive = await GetRecordIdByState(filter, externalRequest, true);
            var recordIdInactive = await GetRecordIdByState(filter, externalRequest, false);

            if(!recordIdActive.HasValue && !recordIdInactive.HasValue) throw new ValidationException("No existe la estructura organizacional base.");

            if (recordIdInactive.HasValue) throw new ValidationException("Se encuentra inactiva la estructura organizacional base.");

            return recordIdActive;
        }

        public async Task<int?> GetMatchingRecordIdServiceBase(int baseId, int? serviceId)
        {
            var recordIdActive = await GetAssociatedData(baseId, serviceId.Value, true);
            var recordIdInactive = await GetAssociatedData(baseId, serviceId.Value, false);

            if (!recordIdActive.HasValue && !recordIdInactive.HasValue) throw new ValidationException("No existe la estructura organizacional base servicio.");

            if (recordIdInactive.HasValue) throw new ValidationException("Se encuentra inactiva la estructura organizacional base servicio.");

            return recordIdActive;
        }
        public Filter Map(Dictionary<string, object?> valuesDictionary)
        {
            var filter = new Filter
            {
                Company = GetValue<Empresa>(valuesDictionary, "empresa"),
                Product = GetValue<Producto>(valuesDictionary, "producto"),
                Branch = GetValue<Sucursal>(valuesDictionary, "sucursal"),
                BusinessUnit = GetValue<UnidadNegocio>(valuesDictionary, "unidad_negocio"),
                Service = GetValue<Servicio>(valuesDictionary, "servicio")
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
        public async Task<BaseServiceResponse> ValidateExternalBaseStructure(ExternalBaseStructure externalRequest)
        {
            var valuesDictionary= await CheckFiltersInvalid(externalRequest);

            var filter = Map(valuesDictionary);
            
            var baseId = await GetMatchingRecordBase(filter,externalRequest);

            int? baseServiceId = null;

            if (externalRequest.ServiceCodeIntOF != null)
            {
                var serviceId = filter.Service.id;
                baseServiceId = await GetMatchingRecordIdServiceBase(baseId.Value,(int) serviceId);
            }

            return new BaseServiceResponse
            {
                BaseId = baseId.Value,
                BaseServiceId = baseServiceId ?? null,
                Description = baseServiceId != null ? filter.Service.descripcion : string.Empty
            };
        }

        private async Task<int?> GetRecordIdByState(Filter filter, ExternalBaseStructure externalRequest, bool isActive)
        {
            var queryArgs = new
            {
                id_empresa = filter.Company.id,
                id_producto = filter.Product.id_producto,
                id_sucursal = filter.Branch.id,
                id_unidad_negocio = filter.BusinessUnit.id,
                estado = isActive         
            };

            var associatedId = await _genericQuery.GetColumnAsync<int?>(DatabaseConstants.SCHEMA_NSF, DatabaseConstants.ORGANIZATIONAL_STRUCTURE_BASE_TABLE, new List<string> { "id" }, queryArgs);

            return associatedId;
        }
        private async Task<int?> GetAssociatedData(int baseId, int serviceId, bool isActive)
        {
            var associatedId = await _genericQuery.GetColumnAsync<int?>(DatabaseConstants.SCHEMA_NSF, DatabaseConstants.ORGANIZATIONAL_STRUCTURE_SERVICE_TABLE, new List<string> { "id" }, new
            {
                id_estructura_organizacional_base = baseId,
                id_servicio = serviceId,
                estado = isActive 
            });

            return associatedId;
        }

        private async Task<Dictionary<string,object?>> CheckFiltersInvalid(ExternalBaseStructure externalRequest)
        {
            var results = await CheckIndividualFiltersAsync(externalRequest);
            var messages = new List<string>();

            if (results[DatabaseConstants.COMPANY_TABLE] == null)
                messages.Add("No se encontró la empresa");

            if (results[DatabaseConstants.PRODUCT_TABLE] == null)
                messages.Add("No se encontró el producto");

            if (results[DatabaseConstants.BRANCH_TABLE] == null)
                messages.Add("No se encontró la sucursal");

            if (results[DatabaseConstants.BUSINESS_UNIT_TABLE] == null)
                messages.Add("No se encontró la unidad de negocio");

            if (results[DatabaseConstants.SERVICE_TABLE] == null && externalRequest.ServiceCodeIntOF != null)
                messages.Add("No se encontró el servicio");

            if (messages.Count > 0)
                throw new ValidationException(string.Join(", ", messages));

            return results;
        }
        public async Task<Dictionary<string, object?>> CheckIndividualFiltersAsync(ExternalBaseStructure externalRequest)
        {
            var results = new Dictionary<string, object?>();

            results[DatabaseConstants.COMPANY_TABLE] = await GetCompanyDataAsync(externalRequest.DocumentNumber);
            results[DatabaseConstants.PRODUCT_TABLE] = await GetProductDataAsync(externalRequest.ProductCodeIntOF);
            results[DatabaseConstants.BRANCH_TABLE] = await GetBranchDataAsync(externalRequest.BranchCodeIntOF);
            results[DatabaseConstants.BUSINESS_UNIT_TABLE] = await GetBusinessUnitDataAsync(externalRequest.BusinessUnitCodeIntOF);
            results[DatabaseConstants.SERVICE_TABLE] = await GetServiceDataAsync(externalRequest.ServiceCodeIntOF);

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
        private async Task<Producto?> GetProductDataAsync(string productCodeIntOF)
        {
            var columns = new List<string> { "id_producto" };
            return await _genericQuery.GetColumnAsync<Producto>(
                DatabaseConstants.SCHEMA_NSF,
                DatabaseConstants.PRODUCT_TABLE,
                columns,
                new { codigo_integracion_of_upper = productCodeIntOF });
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
        private async Task<Servicio?> GetServiceDataAsync(string serviceCodeIntOF)
        {
            var columns = new List<string> { "id","descripcion" };
            return await _genericQuery.GetColumnAsync<Servicio>(
                DatabaseConstants.SCHEMA_NSF,
                DatabaseConstants.SERVICE_TABLE,
                columns,
                new { codigo_integracion_of_upper = serviceCodeIntOF });
        }

    }
}