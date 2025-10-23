using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scharff.API.Utils.Utils.Models;
using Scharff.Application.Queries.User.GetUserByProfileId;
using Scharff.Domain.Response.User;
using Scharff.Infrastructure.PostgreSQL.Queries.User.GetUserByProfileId;
using Swashbuckle.AspNetCore.Annotations;

namespace Scharff.API.Utils.Controllers
{
    [ApiController]
    [Route("api/util")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet(template: "user/GetUserByProfile/{profileId}")]
        [SwaggerResponse(200, "Retorna los usuarios por perfil", typeof(CustomResponse<List<ResponseAllUserByProfileId>>))] 
        [SwaggerResponse(204, "No se encontraron usuarios")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> GetUserByProfileId( string profileId)
        {
            GetAllUserByProfileIdQuery request = new() { profile_Id= profileId };

            var result = await _mediator.Send(request);


            if (result.Count == 0)
            {
                return Ok(new CustomResponse<List<ResponseAllUserByProfileId>>($"No se encontraron registros.", result));
            }
            else
            {
                return Ok(new CustomResponse<List<ResponseAllUserByProfileId>>($"Se encontraron {result.Count} usuario ", result));
            }
        }
    }
}
