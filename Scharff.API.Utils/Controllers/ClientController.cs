using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scharff.API.Utils.Utils.Models;
using Scharff.Application.Queries.Client.VerifyIdentifyClient;
using Swashbuckle.AspNetCore.Annotations;

namespace Scharff.API.Utils.Controllers
{
    [Route("api/util/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(template: "validate/documentType/{documentTypeId}/documentNumber/{documentNumber}")]
        [SwaggerResponse(200, "Retorna datos del cliente en base a su Número de Documento.", typeof(CustomResponse<bool>))]
        [SwaggerResponse(204, "No se encontró el cliente")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> VerifyIdentityClient(int documentTypeId, string documentNumber)
        {
            VerifyIdentityClientQuery request = new() { NumberDocumentIdentity = documentNumber, documentTypeId = documentTypeId };

            var result = await _mediator.Send(request);
            var msg = "";
            if (result)
            {
                msg = $"Se encontró el cliente con Número de Documento: {documentNumber}.";
            }
            else
            {
                msg = $"No se encontraron coincidencias para el Número de Documento ingresado.";
            }
            return Ok(new CustomResponse<bool>(msg, result));
        }
    }
}
