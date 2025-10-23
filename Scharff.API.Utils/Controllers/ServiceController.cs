using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scharff.API.Utils.Utils.Models;
using Scharff.Application.Queries.Service.GetAllService;
using Scharff.Domain.Response.Service.GetAllService;
using Swashbuckle.AspNetCore.Annotations;

namespace Scharff.API.Utils.Controllers
{
    [ApiController]
    [Route("api/util")]
    public class ServiceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(template: "services")]
        [SwaggerResponse(200, "Retorna Servicios", typeof(CustomResponse<List<ResponseGetAllService>>))]
        [SwaggerResponse(204, "No se encontraron servicios")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> GetAllService()
        {
            var result = await _mediator.Send(new GetAllServiceQuery());
            return Ok(new CustomResponse<List<ResponseGetAllService>>($"Se encontraron {result.Count} registros .", result));
        }
    }
}