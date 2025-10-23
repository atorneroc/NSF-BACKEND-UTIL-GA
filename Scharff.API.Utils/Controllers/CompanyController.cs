using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scharff.API.Utils.Utils.Models;
using Scharff.Application.Queries.Company.GetSunatRegimenById;
using Scharff.Domain.Response.Company.GetSunatRegimenById;
using Swashbuckle.AspNetCore.Annotations;

namespace Scharff.API.Utils.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(template: "obtainCompany/{idCompany}")]
        [SwaggerResponse(200, "Retorna datos de empresa por id", typeof(ResponseGetSunatRegimenById))]
        [SwaggerResponse(204, "No se encontraró la empresa")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> GetSunatRegimenById(int idCompany)
        {
            GetSunatRegimenByIdQuery request = new() { Id_Company = idCompany };

            var result = await _mediator.Send(request);

            return Ok(new CustomResponse<ResponseGetSunatRegimenById>(
                $"Se encontraron los datos para el id ",
                result
                ));

        }
    }
}
