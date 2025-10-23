using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scharff.API.Utils.Utils.Models;
using Scharff.Application.Queries.Parameter;
using Scharff.Application.Queries.Parameter.CheckExternalExistenceUser;
using Scharff.Application.Queries.Parameter.GetAllCompanies;
using Scharff.Application.Queries.Parameter.GetAssignmentCustomerByDocumentId;
using Scharff.Application.Queries.Parameter.GetBranchOfficeByCompanyId;
using Scharff.Application.Queries.Parameter.GetBusinessUnitByBranchOfficeId;
using Scharff.Application.Queries.Parameter.GetIgv;
using Scharff.Application.Queries.Parameter.GetIntegrationCodeByIdTypeVoucher;
using Scharff.Application.Queries.Parameter.GetParameterByDetailCode;
using Scharff.Application.Queries.Parameter.GetParameterByGroupId;
using Scharff.Application.Queries.Parameter.GetStoreByBusinessUnitId;
using Scharff.Application.Queries.Parameter.ValidateConfiguredService;
using Scharff.Application.Queries.Parameter.ValidateConfiguredServiceFree;
using Scharff.Application.Queries.Parameter.ValidateExternalBaseStructure;
using Scharff.Application.Queries.Product.GetProductsByIdEmpresaAndIdSucursalAndIdUNegocio;
using Scharff.Application.Queries.UbicacionGeografica.GetUbicacionGeograficaById;
using Scharff.Domain.Response.Parameter.GetAllCompanies;
using Scharff.Domain.Response.Parameter.GetAssignmentCustomerByDocumentId;
using Scharff.Domain.Response.Parameter.GetBranchOfficeByCompanyId;
using Scharff.Domain.Response.Parameter.GetBusinessUnitByBranchOfficeId;
using Scharff.Domain.Response.Parameter.GetIgv;
using Scharff.Domain.Response.Parameter.GetIntegrationCodeByIdTypeVoucher;
using Scharff.Domain.Response.Parameter.GetParameterByCodeDetail;
using Scharff.Domain.Response.Parameter.GetParameterByGroupId;
using Scharff.Domain.Response.Parameter.GetStoreByBusinessUnitId;
using Scharff.Domain.Response.Parameter.ValidateConfiguredService;
using Scharff.Domain.Response.Parameter.ValidateConfiguredServiceFree;
using Scharff.Domain.Response.Parameter.ValidateExternalBaseStructure;
using Scharff.Domain.Response.Product.GetProductsByIdEmpresaAndIdSucursalAndIdUNegocio;
using Scharff.Domain.Response.Security.GetAccessByUser;
using Scharff.Domain.Response.UbicacionGeografica;
using Swashbuckle.AspNetCore.Annotations;

namespace Scharff.API.Utils.Controllers
{
    [ApiController]
    [Route("api/util")]
    public class ParameterController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ParameterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(template: "parameter/{groupId}")]
        [SwaggerResponse(200, "Retorna Parámetros por Grupo", typeof(CustomResponse<List<ResponseGetParameterByGroupId>>))]
        [SwaggerResponse(204, "No se encontró el Grupo de Parámetros")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> GetParameterByGroupId(string groupId)
        {
            GetParameterByGroupIdQuery request = new() { groupId = groupId };

            var result = await _mediator.Send(request);
            return Ok(new CustomResponse<List<ResponseGetParameterByGroupId>>($"Se encontraron {result.Count} registros .", result));
        }

        [HttpGet("enterprises")]
        [SwaggerOperation("Listado de Empresas")]
        [SwaggerResponse(200, "Retorna el listado general de empresas", typeof(CustomResponse<List<ResponseGetAllCompanies>>))]
        [SwaggerResponse(204, "No se encontró el Grupo de Parámetros")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> GetAllCompanies()
        {

            var result = await _mediator.Send(new GetAllCompaniesQuery());
            return Ok(new CustomResponse<List<ResponseGetAllCompanies>>($"Se encontraron {result.Count} empresas .", result));
        }

        [HttpGet("enterprise/{enterpriseId}/branchOffices")]
        [SwaggerOperation("Listado de Sucursales por Empresa")]
        [SwaggerResponse(200, "Retorna listado de sucursales por empresa", typeof(CustomResponse<List<ResponseGetBranchOfficeByCompanyId>>))]
        [SwaggerResponse(204, "No se encontró el grupo de sucursales")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> GetBranchOfficeByCompanyId(int enterpriseId)
        {
            GetBranchOfficeByCompanyIdQuery request = new() { company_id = enterpriseId };

            var result = await _mediator.Send(request);
            return Ok(new CustomResponse<List<ResponseGetBranchOfficeByCompanyId>>($"Se encontraron {result.Count} sucursales.", result));
        }

        [HttpGet("enterprise/{enterpriseId}/branchOffice/{branchOfficeId}/businessUnits")]
        [SwaggerOperation("Listado de Unidades de Negocio por Empresa y Sucursal")]
        [SwaggerResponse(200, "Retorna listado de Unidades de Negocio por Empresa y Sucursal", typeof(CustomResponse<List<ResponseGetBusinessUnitByBranchOfficeId>>))]
        [SwaggerResponse(204, "No se encontró el grupo de unidad de negocio")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> GetBusinessUnitByBranchOfficeId(int enterpriseId, int branchOfficeId)
        {
            GetBusinessUnitByBranchOfficeIdQuery request = new() { CompanyId = enterpriseId, branchOfficeId = branchOfficeId };

            var result = await _mediator.Send(request);
            return Ok(new CustomResponse<List<ResponseGetBusinessUnitByBranchOfficeId>>($"Se encontraron {result.Count} unidades de negocio.", result));
        }

        [HttpGet("enterprise/{enterpriseId}/branchOffice/{branchOfficeId}/businessUnit/{businessUnitId}/stores")]
        [SwaggerOperation("Listado de Productos por Empresa, Sucursal y Unidad de Negocio")]
        [SwaggerResponse(200, "Retorna Listado de Empresa, Sucursal y Unidad de Negocio", typeof(CustomResponse<List<ResponseGetStoreByBusinessUnitId>>))]
        [SwaggerResponse(204, "No se encontró el grupo de productos")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> GetStoreByBusinessUnitId(int enterpriseId, int branchOfficeId, int businessUnitId)
        {
            GetStoreByBusinessUnitIdQuery request = new() { CompanyId = enterpriseId, branchOfficeId = branchOfficeId, businessUnitId = businessUnitId };

            var result = await _mediator.Send(request);
            return Ok(new CustomResponse<List<ResponseGetStoreByBusinessUnitId>>($"Se encontraron {result.Count} productos.", result));
        }

        [HttpGet("igv")]
        [SwaggerOperation("Obtener Igv")]
        [SwaggerResponse(200, "Retorna el Igv", typeof(CustomResponse<List<ResponseGetIgv>>))]
        [SwaggerResponse(204, "No se encontró igv")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> GetIgv()
        {
            var result = await _mediator.Send(new GetIgvQuery());
            return Ok(new CustomResponse<List<ResponseGetIgv>>($"Se encontro {result.Count} igv.", result));
        }

        [HttpGet("enterprise/{enterpriseId}/branchOffice/{branchOfficeId}/businessUnit/{businessUnitId}/products")]
        [SwaggerOperation("Listado de productos por Empresa, Sucursal y Unidad de Negocio")]
        [SwaggerResponse(200, "Retorna Listado de productos", typeof(CustomResponse<List<ResponseGetStoreByBusinessUnitId>>))]
        [SwaggerResponse(204, "No se encontró el grupo de productos")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> GetProductsByIdEmpresaAndIdSucursalAndIdUNegocio(int enterpriseId, int branchOfficeId, int businessUnitId)
        {
            GetProductsByIdEmpresaAndIdSucursalAndIdUNegocioQuery request = new() { id_empresa = enterpriseId, id_sucursal = branchOfficeId, id_unidad_negocio = businessUnitId };

            var result = await _mediator.Send(request);
            return Ok(new CustomResponse<List<ResponseGetProductsByIdEmpresaAndIdSucursalAndIdUNegocio>>($"Se encontro {result.Count} productos.", result));
        }

        [HttpGet("geographiclocation/{geographiclocationId}/Geographiclocations")]
        [SwaggerOperation("Listado de país, departamento")]
        [SwaggerResponse(200, "Retorna Listado de país, departamento, ciudad", typeof(CustomResponse<List<ResponseGetUbicacionGeograficaById>>))]
        [SwaggerResponse(204, "No se encontró el grupo de país")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> GetUbicacionGeograficaById(int geographiclocationId)
        {
            GetUbicacionGeograficaByIdQuery request = new() { id = geographiclocationId };

            var result = await _mediator.Send(request);
            return Ok(new CustomResponse<List<ResponseGetUbicacionGeograficaById>>($"Se encontró {result.Count} productos.", result));
        }

        [HttpGet(template: "parameter/detailCodes")]
        [SwaggerResponse(200, "Retorna parámetros por codigo detalle", typeof(CustomResponse<List<ResponseGetParameterByDetailCode>>))]
        [SwaggerResponse(204, "No se encontraron los parámetros por codigo detalle")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> GetParameterByDetailCode([FromQuery] GetParameterByDetailCodeQuery request)
        {
            var result = await _mediator.Send(request);
            return Ok(new CustomResponse<List<ResponseGetParameterByDetailCode>>($"Se encontraron {result.Count} registros .", result));
        }

        [HttpGet(template: "parameter/external/baseStructure/validation")]
        [SwaggerResponse(200, "Retorna el id de la estructura base del servicio", typeof(CustomResponse<List<ResponseGetParameterByDetailCode>>))]
        [SwaggerResponse(204, "No se encontro la estructura base servicio")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> ExternalBaseStructureValidation([FromQuery] ValidateExternalBaseStructureQuery request)
        {
            var result = await _mediator.Send(request);

            var message = result.BaseServiceId != null && result.BaseServiceId != 0
                ? $"Se encontró el ID de la base: {result.BaseId} y el ID del servicio base: {result.BaseServiceId}."
                : $"Se encontró el ID de la base: {result.BaseId}.";

            return Ok(new CustomResponse<BaseServiceResponse>(message, result));
        }

        [HttpGet(template: "parameter/checkExistenceUser/userEmail/{user_email}")]
        [SwaggerResponse(200, "Retorna el id del usuario si es valido", typeof(CustomResponse<List<ResponseGetParameterByDetailCode>>))]
        [SwaggerResponse(204, "No se encontro el usuario")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> CheckExistenceUser(string user_email)
        {
            CheckExternalExistenceUserQuery request = new() { User_Email = user_email };
            var result = await _mediator.Send(request);
            return Ok(new CustomResponse<string>($"Se encontro el id {result} del usuario ", result));
        }

        [HttpGet(template: "parameter/external/configuredService/validation")]
        [SwaggerResponse(200, "Retorna lista de servicios segun configuracion", typeof(CustomResponse<List<ConfiguredServiceResponse>>))]
        [SwaggerResponse(204, "No se encontraron servicios")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> ConfiguredServiceValidation([FromQuery] ValidateConfiguredServiceQuery request)
        {
            var result = await _mediator.Send(request);

            return Ok(new CustomResponse<List<ConfiguredServiceResponse>>($"Se encontraron {result.Count} servicios.", result));
        }

        [HttpPost(template: "parameter/external/configuredServiceFree/validation")]
        [SwaggerResponse(200, "Retorna lista de servicios segun configuracion gratuita", typeof(CustomResponse<List<ConfiguredServiceFreeResponse>>))]
        [SwaggerResponse(204, "No se encontraron servicios")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> ConfiguredServiceFreeValidation([FromBody] ValidateConfiguredServiceFree[] request)
        {
            var command = new ValidateConfiguredServiceFreeQuery { Queries = request };
            var result = await _mediator.Send(command);

            return Ok(new CustomResponse<List<ConfiguredServiceFreeResponse>>($"Se encontraron {result.Count} servicios.", result));
        }

        [HttpGet(template: "integrationcode/{idTypeVoucher}")]
        [SwaggerOperation("Listado de codigos de integracion por tipo de comprobante")]
        [SwaggerResponse(200, "Retorna el listado de codigos de integracion por tipo de comprobante", typeof(CustomResponse<List<ResponseGetIntegrationCodeByIdTypeVoucher>>))]
        [SwaggerResponse(204, "No se encontró el istado de codigos de integracion por tipo de comprobante")]
        [SwaggerResponse(400, "Ocurrió un error")]
        public async Task<IActionResult> GetIntegrationCodeByIdTypeVoucher(int idTypeVoucher)
        {
            GetIntegrationCodeByIdTypeVoucherQuery request = new() { idTypeVoucher = idTypeVoucher };
            var result = await _mediator.Send(request);
            return Ok(new CustomResponse<List<ResponseGetIntegrationCodeByIdTypeVoucher>>($"Se encontraron {result.Count} resultados .", result));
        }

        [HttpGet("assignmentCustomer/{documentTypeId}/{receiptTypeId}")]
        [SwaggerOperation("Listado de tipos de operación por tipo de documento")]
        [SwaggerResponse(200, "Retorna el listado de tipos de operación por tipo de documento", typeof(CustomResponse<List<ResponseGetAssignmentCustomerByDocumentId>>))]
        [SwaggerResponse(204, "No se encontró el tipos de operación por tipo de documento")]
        [SwaggerResponse(400, "Ocurrió un error")]
        public async Task<IActionResult> GetAssignmentCustomerByDocumentId(int documentTypeId, int receiptTypeId)
        {

            var query = new GetAssignmentCustomerByDocumentIdQuery() { DocumentTypeId = documentTypeId, ReceiptTypeId = receiptTypeId };
            var result = await _mediator.Send(query);

            return Ok(new CustomResponse<List<ResponseGetAssignmentCustomerByDocumentId>>($"Se encontraron {result.Count} resultados .", result));
        }
    }
}