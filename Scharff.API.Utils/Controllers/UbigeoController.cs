using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Scharff.API.Utils.Utils.Models;
using Scharff.Application.Queries.Parameter.GetAllCountryRegion;
using Scharff.Application.Queries.Ubigeo.CheckUbigeoByCode;
using Scharff.Application.Queries.Ubigeo.CheckUbigeoBySapCodes;
using Scharff.Application.Queries.Ubigeo.GetCountryRegionById;
using Scharff.Application.Queries.Ubigeo.GetUbigeoByCode;
using Scharff.Application.Queries.Ubigeo.GetUbigeoByCountry;
using Scharff.Domain.Response.Ubigeo.CheckUbigeoByCode;
using Scharff.Domain.Response.Ubigeo.CheckUbigeoBySapCodes;
using Scharff.Domain.Response.Ubigeo.GetAllCountryRegion;
using Scharff.Domain.Response.Ubigeo.GetByIdCountryRegion;
using Scharff.Domain.Response.Ubigeo.GetUbigeoByCode;
using Scharff.Domain.Response.Ubigeo.GetUbigeoByContry;
using Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.GetByIdCountryRegion;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.RegularExpressions;

namespace Scharff.API.Utils.Controllers
{
    [ApiController]
    [Route("api/util")]
    public class UbigeoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UbigeoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(template: "countries")]
        [SwaggerResponse(200, "Retorna Países y Regiones", typeof(CustomResponse<List<ResponseGetAllCountryRegion>>))]
        [SwaggerResponse(204, "No se encontraron registros")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> GetAllCountryRegion(string? Term)
        {
            var result = await _mediator.Send(new GetAllCountryRegionQuery() { Term = Term});
            return Ok(new CustomResponse<List<ResponseGetAllCountryRegion>>($"Se encontraron {result.Count} registros .", result));
        }

        [HttpGet(template: "ubigeo/{ubigeoCode}")]
        [SwaggerResponse(200, "Retorna Departamento, Provincia y Distrito", typeof(CustomResponse<List<ResponseGetUbigeoByCode>>))]
        [SwaggerResponse(204, "No se encontraron registros")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> GetUbigeoByCode(string ubigeoCode)
        {
            var codesList = ubigeoCode.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

            GetUbigeoByCodeQuery request = new() { ubigeoCode = codesList };

            var result = await _mediator.Send(request);
            return Ok(new CustomResponse<List<ResponseGetUbigeoByCode>>($"Se encontraron {result.Count} registros .", result));
        }

        [HttpGet(template: "checkUbigeo/ubigeoCode/{ubigeo_code}")]
        [SwaggerResponse(200, "Retorna pais, departamento, provincia y distrito segun codigo de ubigeo", typeof(CustomResponse<List<ResponseCheckUbigeoByCode>>))]
        [SwaggerResponse(204, "No se encontraron registros")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> CheckUbigeoByCode(string ubigeo_code)
        {
            CheckUbigeoByCodeQuery request = new() { Ubigeo_code = ubigeo_code };

            var result = await _mediator.Send(request);
            var message = (result == null || !result.Any())
                         ? $"No se encontró el ubigeo: {ubigeo_code}."
                         : $"Se encontró el ubigeo: {ubigeo_code}.";

            var responseData = (result == null || !result.Any()) ? null : result;

            return Ok(new CustomResponse<List<ResponseCheckUbigeoByCode>>
            {
                Message = message,
                Data = responseData,
                Error = null
            });
        }

        [HttpGet(template: "countries/{id}")]
        [SwaggerResponse(200, "Retorna Países y Regiones por id ", typeof(CustomResponse<List<ResponseGetByIdCountryRegion>>))]
        [SwaggerResponse(204, "No se encontraron registros")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> GetCountryRegionById(int id)
        {
            GetCountryRegionByIdQuery request = new() {  id = id};
            var result = await _mediator.Send(request);
            return Ok(new CustomResponse<List<ResponseGetByIdCountryRegion>>($"Se encontraron {result.Count} registros .", result));
        }

        [HttpGet(template: "ubigeoByCountry/filter")]
        [SwaggerResponse(200, "Retorna lista de ubigeos por pais", typeof(CustomResponse<List<ResponseGetUbigeoByCountry>>))]
        [SwaggerResponse(204, "No se encontraron registros")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> GetUbigeoByCountry([FromQuery, BindRequired] string term, [FromQuery] int size)
        {
            GetUbigeoByCountryQuery request = new() { term = term, size = size };
            var result = await _mediator.Send(request);
            return Ok(new CustomResponse<List<ResponseGetUbigeoByCountry>>($"Se encontraron {result.Count} registros .", result));
        }

        [HttpGet(template: "checkUbigeo/sapCodes/{codigo_sap_pais}/{codigo_sap_region}")]
        [SwaggerResponse(200, "Retorna la información de ubicación según códigos SAP de país y región", typeof(CustomResponse<List<ResponseUbigeoLocationBySapCodes>>))]
        [SwaggerResponse(204, "No se encontraron registros")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> CheckUbigeoBySapCodes(string codigo_sap_pais, string codigo_sap_region)
        {
            CheckUbigeoBySapCodesQuery request = new()
            {
                SapCountryCode = codigo_sap_pais,
                SapRegionCode = codigo_sap_region
            };

            var result = await _mediator.Send(request);
            var message = (result == null || !result.Any())
                         ? $"No se encontró ubicación con código SAP país: {codigo_sap_pais} y región: {codigo_sap_region}."
                         : $"Se encontró ubicación con código SAP país: {codigo_sap_pais} y región: {codigo_sap_region}.";

            var responseData = (result == null || !result.Any()) ? null : result;

            return Ok(new CustomResponse<List<ResponseUbigeoLocationBySapCodes>>
            {
                Message = message,
                Data = responseData,
                Error = null
            });
        }
    }
}