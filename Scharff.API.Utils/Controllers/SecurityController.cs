using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scharff.API.Utils.Utils.Models;
using Scharff.Application.Queries.Security.GetAccessByUser;
using Scharff.Application.Queries.Security.GetVerifyMenuRoutByUserRout;
using Scharff.Domain.Response.Security.GetAccessByUser;
using Swashbuckle.AspNetCore.Annotations;

namespace Scharff.API.Utils.Controllers
{
    [ApiController]
    [Route("api/security")]
    public class SecurityController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SecurityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(template: "menubyuser")]
        [SwaggerResponse(200, "Retorna Accesos al Sistema", typeof(CustomResponse<List<ResponseAccess>>))]
        [SwaggerResponse(204, "No se encontraron permisos")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> GetMenuByUser(string user_email)
        {
            GetAccessByUserQuery request = new() { User_Email = user_email };
            var result = await _mediator.Send(request);

            return Ok(new CustomResponse<List<ResponseAccess>>($"Se encontraron {result.Count} registros .", result));
        }

        [HttpGet(template: "verifymenubyuser")]
        [SwaggerResponse(200, "Retorna Acceso al Menú", typeof(CustomResponse<int>))]
        [SwaggerResponse(204, "Usted no puede acceder a esta opción")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> GetVerifyMenuRoutByUserRout(string user_email, string route)
        {
            GetVerifyMenuRoutByUserRoutQuery request = new() { User_Email = user_email, Route = route };
            int cant = await _mediator.Send(request);

            return Ok(new CustomResponse<int>($"Acceso correcto.", cant));
        }
    }
}