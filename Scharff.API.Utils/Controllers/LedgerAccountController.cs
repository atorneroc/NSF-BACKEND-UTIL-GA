using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scharff.API.Utils.Utils.Models;
using Scharff.Application.Queries.Service.GetAllLedgerAccount;
using Scharff.Domain.Response.LedgerAccount.GetAllLedgerAccount;
using Swashbuckle.AspNetCore.Annotations;

namespace Scharff.API.Utils.Controllers
{
    [ApiController]
    [Route("api/util")]
    public class LedgerAccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LedgerAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(template: "ledgeraccounts")]
        [SwaggerResponse(200, "Retorna Cuentas Contables", typeof(CustomResponse<List<ResponseGetAllLedgerAccount>>))]
        [SwaggerResponse(204, "No se encontraron Cuentas Contables")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> GetAllLedgerAccount()
        {
            var result = await _mediator.Send(new GetAllLedgerAccountQuery());
            return Ok(new CustomResponse<List<ResponseGetAllLedgerAccount>>($"Se encontraron {result.Count} registros .", result));
        }
    }
}