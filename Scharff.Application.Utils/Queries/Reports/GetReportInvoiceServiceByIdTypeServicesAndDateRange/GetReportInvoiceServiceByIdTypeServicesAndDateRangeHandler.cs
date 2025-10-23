using MediatR;
using Scharff.Infrastructure.Http.Queries.Reports.GetReportInvoiceServiceByIdTypeServicesAndDateRange;
using System.Data;

using OfficeOpenXml;
using OfficeOpenXml.Style;
using Scharff.Domain.Response.Reports.GetReportInvoiceServiceByIdTypeServicesAndDateRangeQuery;
using Azure;
using Scharff.Domain.Utils.Exceptions;
using IronPdfEngine.Proto;

namespace Scharff.Application.Queries.Reports.ReportInvoiceServiceByIdTypeServicesAndDateRange
{
    public class GetReportInvoiceServiceByIdTypeServicesAndDateRangeHandler : IRequestHandler<GetReportInvoiceServiceByIdTypeServicesAndDateRangeQuery, ResponseFile>
    {

        private readonly IGetReportInvoiceServiceByIdTypeServicesAndDateRange _getReportInvoiceService;
        public GetReportInvoiceServiceByIdTypeServicesAndDateRangeHandler(IGetReportInvoiceServiceByIdTypeServicesAndDateRange GetReportInvoiceService)
        {
            _getReportInvoiceService    = GetReportInvoiceService;

        }
        public async Task<ResponseFile> Handle(GetReportInvoiceServiceByIdTypeServicesAndDateRangeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Obtener los datos del informe
                DataSet dtReport; 
                string reportTitle = "REPORTE";
                string reportNombre= "";

                if (request.codTypeReport.Equals("TREP1"))
                {
                    dtReport = await _getReportInvoiceService.GetReportfreight(request.issue_Date_Start, request.issue_Date_End);
                    reportNombre = "FLETE";
                    reportTitle = reportTitle + " DE " + reportNombre;
                }
                else if (request.codTypeReport.Equals("TREP2"))
                {
                    dtReport = await _getReportInvoiceService.GetReportFee(request.issue_Date_Start, request.issue_Date_End);
                    reportNombre = "IMPUESTO";
                    reportTitle = reportTitle + " DE " + reportNombre;
                }
                else if (request.codTypeReport.Equals("TREP3"))
                {
                    dtReport = await _getReportInvoiceService.GetReportAcreditations(request.issue_Date_Start, request.issue_Date_End);
                    reportNombre = "ACREDITACIONES";
                    reportTitle = reportTitle + " DE "+ reportNombre;
                }
                else
                {
                    throw new ArgumentException("Tipo de reporte no válido.");
                }
                // Verificar si se encontraron datos    

                if (dtReport == null || dtReport.Tables.Count == 0 || dtReport.Tables[0].Rows.Count == 0)
                {
                    throw new NotFoundException("No se encontraron datos para descargar.");
                }

                // Crear el archivo Excel
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (var package = new ExcelPackage())
                    {
                        // Agregar una hoja de trabajo al libro de Excel
                        var worksheet = package.Workbook.Worksheets.Add(reportTitle);

                        worksheet.View.ShowGridLines = false;
                        // Agregar el nombre de la empresa en la celda A1

                        worksheet.Cells["A1:E1"].Merge = true;
                        worksheet.Cells["A1"].Value = "SCHARFF COURIER INTERNACIONAL";
                        worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells["A1"].Style.Font.Bold = true;

                        worksheet.Cells["A2:E2"].Merge = true;
                        worksheet.Cells["A2"].Value = DateTime.Now.ToString("dd/MM/yyyy");
                        worksheet.Cells["A2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells["A2"].Style.Font.Bold = true;



                    worksheet.Cells["A3:H3"].Merge = true;
                        worksheet.Cells["A3"].Value = reportTitle;
                        worksheet.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["A3"].Style.Font.Bold = true;
                        worksheet.Cells["A3"].Style.Font.Size = 14;

                        // Definir la fila inicial para escribir los datos
                        int rowIndex = 5; 
                        // Agregar la cabecera del informe
                         DataTable dtHeader = dtReport.Tables[0];
                        for (int i = 0; i < dtHeader.Columns.Count; i++)
                        { 
                            var cell = worksheet.Cells[rowIndex, i + 1];
                            cell.Value = dtHeader.Rows[0][i]; // Asignar los valores de la cabecera del informe
                            cell.Style.Font.Bold = true;  
                            cell.Style.Font.Size = 12;  
                        }
                        rowIndex++; 

                        // Definir la fila inicial para escribir los datos
                        int rowIndex1 = 6;

                        // Agregar el cuerpo del informe
                        DataTable dtBody = dtReport.Tables[1];
                        foreach (DataRow row in dtBody.Rows)
                        {
                            for (int i = 0; i < dtBody.Columns.Count; i++)
                            {
                                worksheet.Cells[rowIndex1, i + 1].Value = row[i]; // Asignar los valores del cuerpo del informe
                            }
                        rowIndex1++;
                        }
                        worksheet.Cells.AutoFitColumns(); 

                    // Guardar el archivo de Excel en un MemoryStream
                    var stream = new MemoryStream(package.GetAsByteArray());
                        // Construir el nombre del archivo con las fechas
                        string startDate = request.issue_Date_Start.ToString("yyyyMMdd");
                        string endDate = request.issue_Date_End.ToString("yyyyMMdd");
                        string fileName = $"Reporte_{reportNombre}_{startDate}_{endDate}.xlsx";

                        // Devolver el MemoryStream como resultado
                        ResponseFile response = new ResponseFile
                        {
                            File = stream.ToArray(), // Convertir el MemoryStream a un arreglo de bytes
                            Name = fileName, // Nombre del archivo
                            Type = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" // Tipo de contenido del archivo
                        };


                        return response;


                    }
            }
            catch (Exception)
            {
                throw;
            }
        }

         

    }
}
