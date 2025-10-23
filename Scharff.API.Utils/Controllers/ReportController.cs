using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scharff.API.Utils.Utils.Models; 
using Scharff.Application.Queries.Reports.GetConsolidatedReport;
using Scharff.Application.Queries.Reports.GenerateReportServiceOrderCourier;
using Scharff.Application.Queries.Reports.Report;
using Scharff.Application.Queries.Reports.ReportInvoiceServiceByIdTypeServicesAndDateRange;
using Scharff.Domain.Request.Report;
using Scharff.Domain.Response.Reports.GetConsolidatedReport;
using Scharff.Domain.Response.Reports.GetReportInvoiceServiceByIdTypeServicesAndDateRangeQuery;
using Swashbuckle.AspNetCore.Annotations;
using Azure;

namespace Scharff.API.Utils.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetReport")]
        public async Task<IActionResult> GetReport([FromQuery] GetReportQuery request)
        {
            var result = await _mediator.Send(request);
            return File(result, "application/pdf", "Reporte");
        }

        [HttpGet(template: "GetReportExcel/codeTypeReport/{codTypeReport}/issueDateStart/{issueDateStart}/issueDateEnd/{issueDateEnd}")]
        [SwaggerOperation("Obtener el listado por tipo de reporte seleccionado")]
        [SwaggerResponse(200, "Descarga datos en archivo en formato excel", typeof(ResponseFile))]
        [SwaggerResponse(204, "No se encontraron el detalle para reporte")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> GetReportInvoiceServiceExcel(string codTypeReport, DateTime issueDateStart, DateTime issueDateEnd)
        {
            GetReportInvoiceServiceByIdTypeServicesAndDateRangeQuery request = new GetReportInvoiceServiceByIdTypeServicesAndDateRangeQuery
            {
                codTypeReport = codTypeReport,
                issue_Date_Start = issueDateStart,
                issue_Date_End = issueDateEnd
            };

            // Enviar la solicitud al manejador para obtener el archivo
            var response = await _mediator.Send(request);

            if (response.File == null)
            {
                // Si el archivo es nulo, devolver una respuesta 204 (No Content)
                return NoContent();
            }

            // Crear un MemoryStream a partir del archivo
            var stream = new MemoryStream(response.File);

            // Devolver el archivo descargable al cliente
            return File(stream, response.Type, response.Name);
        }


        [HttpPost(template: "GenerateReportExcel/ServiceOrderCourier")]
        [SwaggerOperation("Genera reporte por los parametros requeridos")]
        [SwaggerResponse(200, "Descarga datos en archivo en formato excel", typeof(ResponseFile))]
        [SwaggerResponse(204, "No se encontraron el detalle para reporte")]
        [SwaggerResponse(400, "Ocurrió un error de validación")]
        public async Task<IActionResult> GenerateReportServiceOrderCourier(GetReportServiceOrderCourierQuery request)
        {
            var response = await _mediator.Send(request);

            if (response.File == null)
            {
                // Si el archivo es nulo, devolver una respuesta 204 (No Content)
                return NoContent();
            }
            // Crear un MemoryStream a partir del archivo
            var stream = new MemoryStream(response.File);
            // Devolver el archivo descargable al cliente
            return File(stream, response.Type, response.Name);
        }

        [HttpGet(template: "GenerateConsolidatedReport/{idServiceOrder}")]
        [SwaggerOperation("Genera reporte por los parametros requeridos")]
        [SwaggerResponse(200, "Muestra Datos en Base a ID Orden de Servicio", typeof(ResponseFile))]
        [SwaggerResponse(204, "No se encontró Informacion")]
        [SwaggerResponse(400, "Ocurrio un error de validación")]
        public async Task<IActionResult> GetConsolidatedReportQuery(int idServiceOrder)
        {
            GetConsolidatedReportQuery request = new()
            {
                service_order_id = idServiceOrder
            };

            var result = await _mediator.Send(request);


            // Consumir el método GenerateReportServiceOrderCourier
            GetReportServiceOrderCourierQuery reportRequest = new GetReportServiceOrderCourierQuery
            { 
                 id = result.Id,
                 company_id = result.company_id,
                 client_id = result.client_id,
                 author = result.author,
                 service_category_id = result.service_category_id,
                 category_id_invoice = result.category_id_invoice,
                 guide_list = result.guide_gist.Select(guide => new Guide
                 {
                     billable_order_id = guide.billable_order_id
                 }).ToList()
            };

            var reportResponse = await _mediator.Send(reportRequest);

            if (reportResponse.File == null)
            {
                // Si el archivo es nulo, devolver una respuesta 204 (No Content)
                return NoContent();
            }

            // Crear un MemoryStream a partir del archivo
            var stream = new MemoryStream(reportResponse.File);

            // Devolver el archivo descargable al cliente
            return File(stream, reportResponse.Type, reportResponse.Name);
        }
    }


}
