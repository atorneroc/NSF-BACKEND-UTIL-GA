using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scharff.API.Utils.Utils.Models;
using Scharff.Application.Queries.PaymentVoucherConfiguration.GetPaymentVoucherConfiguration;
using Scharff.Domain.Response.PaymentVoucherConfiguration;
using Swashbuckle.AspNetCore.Annotations;

namespace Scharff.API.Utils.Controllers
{
    [ApiController]
    [Route("api")]
    public class PaymentVoucherConfigurationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentVoucherConfigurationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(template: "paymentVoucherConfiguration/{Id_Payment_Voucher_Type}")]
        [SwaggerResponse(200, "Retorna Configuraciones de Comprobante Pago", typeof(CustomResponse<ResponsePaymentVoucherConfiguration>))]
        [SwaggerResponse(204, "No se encontró Configuraciones de Comprobante Pago")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> GetPaymentVoucherConfiguration(int Id_Payment_Voucher_Type)
        {
            GetPaymentVoucherConfigurationQuery request = new() { Id_Payment_Voucher_Type = Id_Payment_Voucher_Type };
            var result = await _mediator.Send(request);
            return Ok(new CustomResponse<ResponsePaymentVoucherConfiguration>($"Se encontro registro .", result));
        }
    }
}
