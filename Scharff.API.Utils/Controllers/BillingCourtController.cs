using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scharff.API.Utils.Utils.Models;
using Scharff.Application.Queries.BillingCourt.GetBusinessUnitByIdCompany;
using Scharff.Application.Queries.BillingCourt.GetProductByIdBusinessUnitIdCompany;
using Scharff.Domain.Response.BillingCourt;
using Swashbuckle.AspNetCore.Annotations;

namespace Scharff.API.Utils.Controllers
{
    [ApiController]
    [Route("api/util")]
    public class BillingCourtController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BillingCourtController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("enterprise/{id_company}/businessUnitDOCv2")]
        [SwaggerOperation("Listado de Unidad de Negocio por Empresa")]
        [SwaggerResponse(200, "Retorna Listado de Unidad de Negocio", typeof(CustomResponse<List<ResponseGetBusinessUnitByIdCompany>>))]
        [SwaggerResponse(204, "No se encontro Unidad de Negocio")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> GetBusinessUnitByIdCompany(int id_company)
        {
            GetBusinessUnitByIdCompanyQuery request = new() { Id_Company = id_company};

            var result = await _mediator.Send(request);
            return Ok(new CustomResponse<List<ResponseGetBusinessUnitByIdCompany>>($"Se encontro {result.Count} productos.", result));
        }

        [HttpGet("enterprise/{id_company}/businessUnit/{id_business_unit}/product")]
        [SwaggerOperation("Listado de Producto por Empresa y Unidad de Negocio")]
        [SwaggerResponse(200, "Retorna Listado de Producto", typeof(CustomResponse<List<ResponseGetBusinessUnitByIdCompany>>))]
        [SwaggerResponse(204, "No se encontro Unidad de Negocio")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> GetBusinessUGetProductByIdBusinessUnitIdCompanynitByIdCompany(int id_company, int id_business_unit)
        {
            GetProductByIdBusinessUnitIdCompanyQuery request = new() { Id_Company = id_company, Id_Business_Unit = id_business_unit };

            var result = await _mediator.Send(request);
            return Ok(new CustomResponse<List<ResponseGetProductByIdBusinessUnitIdCompany>>($"Se encontro {result.Count} productos.", result));
        }
    }
}