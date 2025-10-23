using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scharff.API.Utils.Utils.Models;
using Scharff.Application.Commands.ExchangeRate.RegisterExchangeRate;
using Scharff.Application.Commands.ExchangeRate.UpdateExchangeRate;
using Scharff.Application.Queries.ExchangeRate.GetAllExchangeRate;
using Scharff.Application.Queries.ExchangeRate.GetExchangeRateBroadCast;
using Scharff.Application.Queries.ExchangeRate.GetExchangeRateById;
using Scharff.Domain.Response.ExchangeRate.GetAllExchangeRate;
using Scharff.Domain.Response.ExchangeRate.GetExchangeRateBroadCast;
using Scharff.Domain.Response.ExchangeRate.GetExchangeRateById;
using Scharff.Domain.Response.Utils;
using Swashbuckle.AspNetCore.Annotations;

namespace Scharff.API.Utils.Controllers
{
    [ApiController]
    [Route("api/util")]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ExchangeRateController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet(template: "exchangerate/byDate/{dateExchangeRate}")]
        [SwaggerResponse(200, "Retorna un tipo de cambio en base a su fecha de emisión", typeof(CustomResponse<ResponseGetExchangeRateBroadCast>))]
        [SwaggerResponse(204, "No se encontró el tipo de cambio en la fecha indicada")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> GetExchangeRateByBroadCast(DateTime dateExchangeRate)
        {
            GetExchangeRateBroadCastQuery request = new() { broadCast = dateExchangeRate };

            var result = await _mediator.Send(request);
            return Ok(new CustomResponse<ResponseGetExchangeRateBroadCast>($"Se encontró el tipo de cambio con la fecha de emisión: {dateExchangeRate.ToString("yyyy/MM/dd")}.", result));
        }

        [HttpGet(template: "exchangerates")]
        [SwaggerResponse(200, "Retorna listado de tipo de cambio", typeof(CustomResponse<PaginatedResponse<ResponseGetAllExchangeRate>>))]
        [SwaggerResponse(204, "No se encontraron tipo de cambios")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> GetAllExchangeRate([FromQuery] GetAllExchangeRateQuery request)
        {
            var result = await _mediator.Send(request);
            return Ok(new CustomResponse<PaginatedResponse<ResponseGetAllExchangeRate>>($"Se encontraron {result.Total_rows} Tipos de cambio.", result));
        }

        [HttpPost(template: "exchangerate")]
        [SwaggerOperation("Registrar tipo de cambio")]
        [SwaggerResponse(200, "Los datos se han registrado correctamente")]
        [SwaggerResponse(204, "No se encontró el contacto")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> RegisterExchangeRate([FromBody] RegisterExchangeRateCommand request)
        {
            var result = await _mediator.Send(request);

            return Ok(new CustomResponse<int>(
                $"Se registró el tipo de cambio con id: {result}.",
                result));
        }

        [HttpPut(template: "exchangerate/{id}")]
        [SwaggerOperation("Actualiza tipo de cambio")]
        [SwaggerResponse(200, "Los datos se han actualizado correctamente")]
        [SwaggerResponse(204, "No se encontró el contacto")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> UpdateExchangeRate(int id, [FromBody] UpdateExchangeRateCommand request)
        {
            request.id = id;
            var result = await _mediator.Send(request);

            return Ok(new CustomResponse<int>(
                $"Se actualizo el tipo de cambio con id: {id}.",
                result));
        }

        [HttpGet(template: "exchangerate/{id}")]
        [SwaggerResponse(200, "Retorna tipo de cambio", typeof(CustomResponse<ResponseGetExchangeRateById>))]
        [SwaggerResponse(204, "No se encontro tipo de cambio")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> GetExchangeRateById(int id)
        {
            GetExchangeRateByIdQuery request = new() { id = id };
            var result = await _mediator.Send(request);
            return Ok(new CustomResponse<ResponseGetExchangeRateById>($"Se enconto Tipo de cambio.", result));
        }
    }
}
