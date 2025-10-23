using MediatR;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using Scharff.Application.Helpers;
using Scharff.Domain.Request.Report;
using Scharff.Domain.Response.Reports.GetReportInvoiceServiceByIdTypeServicesAndDateRangeQuery;
using Scharff.Infrastructure.PostgreSQL.Queries.Reports.GetReportServiceOrderCourier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Scharff.Application.Queries.Reports.GenerateReportServiceOrderCourier
{
    public class GetReportServiceOrderCourierHandler : IRequestHandler<GetReportServiceOrderCourierQuery, ResponseFile>
    {
        private readonly IGetReportServiceOrderCourierQuery _getReportServiceOrderCourierQuery;
        public GetReportServiceOrderCourierHandler(IGetReportServiceOrderCourierQuery getReportServiceOrderCourierQuery)
        {
            _getReportServiceOrderCourierQuery = getReportServiceOrderCourierQuery;
        }
        public async Task<ResponseFile> Handle(GetReportServiceOrderCourierQuery request, CancellationToken cancellationToken)
        { 
            DataSet ds = await _getReportServiceOrderCourierQuery.GetReportServiceOrderCourier(request);
             



            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Crear un nuevo paquete de Excel
            using (var package = new ExcelPackage())
            {
                // Agregar una nueva hoja de trabajo al paquete de Excel
                var worksheet = package.Workbook.Worksheets.Add("Hoja1");

                // Obtén las tablas del DataSet
                DataTable headerTable = ds.Tables[0];
                DataTable dataTable = ds.Tables[1];



                int index_column_cab = 10;
                int index_column_det = 9;

                // Agrega los datos de la tabla de cabecera a la hoja de trabajo
                for (int i = index_column_cab; i < headerTable.Columns.Count; i++)
                {
                    //worksheet.Cells[1, i + 1].Value = headerTable.Columns[i].ColumnName;
                    worksheet.Cells[10, i - index_column_det].Value = headerTable.Rows[0][i];
                }

                // Agregar el nombre de la empresa en la celda A1
                string company = dataTable.Rows[0][5].ToString(); 
                string cab_client_id = dataTable.Rows[0][0].ToString(); 
                string cab_company_name_client = dataTable.Rows[0][1].ToString(); 
                string cab_identity_document_number_client = dataTable.Rows[0][2].ToString(); 
                string cab_phone_client = dataTable.Rows[0][3].ToString(); 
                string cab_Fax_client = dataTable.Rows[0][4].ToString();  
                string cab_client_address = dataTable.Rows[0][6].ToString();  
                 
                decimal sum_Package_weight = 0;
                decimal sum_Package_weight_volumetric = 0;
                decimal sum_Usd_Tarifa = 0;
                decimal sum_Usd_Fuel = 0;
                decimal sum_Acreditacion = 0;
                decimal sum_Usd_Neto = 0;  
                decimal sum_Igv_Descuento_Global = 0;   
                

                foreach (DataRow fila in dataTable.Rows)
                {
                    sum_Package_weight += (fila["package_weight"] == DBNull.Value) ? 0 : Convert.ToDecimal(fila["package_weight"]);
                    sum_Package_weight_volumetric += (fila["package_weight_volumetric"] == DBNull.Value) ? 0 : Convert.ToDecimal(fila["package_weight_volumetric"]);
                    sum_Acreditacion += (fila["acreditacion"] == DBNull.Value) ? 0 : Convert.ToDecimal(fila["acreditacion"]);
                    sum_Usd_Tarifa += (fila["usd_tarifa"] == DBNull.Value) ? 0 : Convert.ToDecimal(fila["usd_tarifa"]);
                    sum_Usd_Fuel += (fila["usd_fuel"] == DBNull.Value) ? 0 : Convert.ToDecimal(fila["usd_fuel"]);
                    sum_Usd_Neto += (fila["usd_neto"] == DBNull.Value) ? 0 : Convert.ToDecimal(fila["usd_neto"]);
                    sum_Igv_Descuento_Global += (fila["igv_descuento_global"] == DBNull.Value) ? 0 : Convert.ToDecimal(fila["igv_descuento_global"]);
                    //cantidadRegistros++;
                }

                //  Ocultar las líneas de la cuadrícula
                worksheet.View.ShowGridLines = false;

                // Agregar el nombre de la empresa en la celda A1
                worksheet.Cells["B1:M1"].Merge = true;
                worksheet.Cells["B1"].Value = company ;
                worksheet.Cells["B1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                worksheet.Cells["B1"].Style.Font.Bold = true;
                worksheet.Cells["B1"].Style.Font.Size = 14;



                worksheet.Cells["B3:J3"].Merge = true;
                worksheet.Cells["B3"].Value = "Señor (es):";
                worksheet.Cells["B3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left; 
                worksheet.Cells["B3"].Style.Font.Size = 12;
                 
                worksheet.Cells["B4:J4"].Merge = true;
                worksheet.Cells["B4"].Value = cab_client_id +" " + cab_company_name_client;
                worksheet.Cells["B4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left; 
                worksheet.Cells["B4"].Style.Font.Size = 11;

                worksheet.Cells["B5:J5"].Merge = true;
                worksheet.Cells["B5"].Value = cab_client_address ;
                worksheet.Cells["B5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                worksheet.Cells["B5"].Style.Font.Size = 11;


                worksheet.Cells["B6:D6"].Merge = true;
                worksheet.Cells["B6"].Value = cab_identity_document_number_client;
                worksheet.Cells["B6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                worksheet.Cells["B6"].Style.Font.Size = 11;


                worksheet.Cells["E6:G6"].Merge = true;
                worksheet.Cells["E6"].Value = "TELEFONO: " + cab_phone_client;
                worksheet.Cells["E6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                worksheet.Cells["E6"].Style.Font.Size = 11;

                worksheet.Cells["H6:J6"].Merge = true;
                worksheet.Cells["H6"].Value = "FAX: " + cab_Fax_client;
                worksheet.Cells["H6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                worksheet.Cells["H6"].Style.Font.Size = 11;


                // Agregar el subtitulo reporte
                worksheet.Cells["B8:M8"].Merge = true;
                worksheet.Cells["B8"].Value = "RELACION DE GUIAS POR SERVICIO DE MENSAJERIA INTERNACIONAL CORRESPONDIENTE";
                worksheet.Cells["B8"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                worksheet.Cells["B8"].Style.Font.Bold = true;
                worksheet.Cells["B8"].Style.Font.Size = 12;

                // Obtener la fecha y hora UTC actual
                DateTime now = DateTime.Now;

                // Convertir a la zona horaria de Lima usando tu helper
                DateTime limaDateTime = DateHelper.ConvertToLimaTimeZone(now);

                // Formatear la fecha y la hora
                string date = limaDateTime.ToString("dd/MM/yyyy");
                string time = limaDateTime.ToString("HH:mm");

                // Agregar la fecha en la celda K3
                worksheet.Cells["K3:M3"].Merge = true;
                worksheet.Cells["K3"].Value = "Fecha : " + date;
                worksheet.Cells["K3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left; 
                worksheet.Cells["K3"].Style.Font.Size = 11;

                // Agregar la Hora en la celda K4
                worksheet.Cells["K4:M4"].Merge = true;
                worksheet.Cells["K4"].Value = "Hora : "+ time;
                worksheet.Cells["K4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left; 
                worksheet.Cells["K4"].Style.Font.Size = 11;

                // Agregar la pagina en la celda K5
                worksheet.Cells["K5:M5"].Merge = true;
                worksheet.Cells["K5"].Value = "Pagina 1 de 1";
                worksheet.Cells["K5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left; 
                worksheet.Cells["K5"].Style.Font.Size = 11;


                // Agregar el usuario responsable en la celda L6
                string author = request.author; // Inicializa la variable author con una cadena vacía


                worksheet.Cells["K6:M6"].Merge = true;
                worksheet.Cells["K6"].Value = "Responsable : " + author;
                worksheet.Cells["K6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left; 
                worksheet.Cells["K6"].Style.Font.Size = 11;


                // Agrega los datos de la tabla de datos a la hoja de trabajo
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    for (int j = index_column_cab; j < dataTable.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 11, j - index_column_det].Value = dataTable.Rows[i][j];
                    }
                }

                // Crear una tabla con los datos
                var range = worksheet.Cells[10, 1, dataTable.Rows.Count + 10, dataTable.Columns.Count - index_column_cab];
                var table = worksheet.Tables.Add(range, "TableReporteOSC");

                // Aplicar el estilo a la tabla
                table.TableStyle = TableStyles.Dark8;
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[10, 1, 10, dataTable.Columns.Count - index_column_cab].Style.Font.Bold = true;

                //-------------------------------------------

                var totalRow = dataTable.Rows.Count + 11;

                var total = dataTable.Rows.Count;

                worksheet.Cells[totalRow + 1, 1].Value = "Nro Guias:";
                worksheet.Cells[totalRow + 1, 1].Style.Font.Bold = true;
                worksheet.Cells[totalRow + 1, 2].Value = total;
                worksheet.Cells[totalRow + 1, 2].Style.Font.Bold = true;
                worksheet.Cells[totalRow + 1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                //----------------------------------------------------- 
                 
                worksheet.Cells[totalRow + 0, 6, totalRow + 0, 17].Merge = true;
                worksheet.Cells[totalRow + 0, 6].Value = "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";

                worksheet.Cells[totalRow + 1, 6].Value = "Total Cliente:";
                worksheet.Cells[totalRow + 1, 6].Style.Font.Bold = true;

                worksheet.Cells[totalRow + 1, 9].Value = sum_Package_weight;
                worksheet.Cells[totalRow + 1, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[totalRow + 1, 9].Style.Numberformat.Format = "0.00";

                worksheet.Cells[totalRow + 1, 10].Value = sum_Package_weight_volumetric;
                worksheet.Cells[totalRow + 1, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[totalRow + 1, 10].Style.Numberformat.Format = "0.00";

                worksheet.Cells[totalRow + 1, 11].Value = sum_Usd_Tarifa;
                worksheet.Cells[totalRow + 1, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[totalRow + 1, 11].Style.Numberformat.Format = "0.00";


                worksheet.Cells[totalRow + 1, 15].Value = sum_Usd_Fuel;
                worksheet.Cells[totalRow + 1, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[totalRow + 1, 15].Style.Numberformat.Format = "0.00";

                worksheet.Cells[totalRow + 1, 16].Value = sum_Acreditacion;
                worksheet.Cells[totalRow + 1, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[totalRow + 1, 16].Style.Numberformat.Format = "0.00";

                worksheet.Cells[totalRow + 1, 17].Value = sum_Usd_Neto;
                worksheet.Cells[totalRow + 1, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[totalRow + 1, 17].Style.Numberformat.Format = "0.00";

                //----------------------------------------------------- 

                worksheet.Cells[totalRow + 3, 14].Value = "Imp Neto:"; 
                worksheet.Cells[totalRow + 3, 15].Value = "USD"; 
                worksheet.Cells[totalRow + 3, 17].Value = sum_Usd_Neto;
                worksheet.Cells[totalRow + 3, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[totalRow + 3, 17].Style.Numberformat.Format = "0.00";


                worksheet.Cells[totalRow + 4, 14].Value = "IGV:";
                worksheet.Cells[totalRow + 4, 15].Value = "USD";
                worksheet.Cells[totalRow + 4, 17].Value = sum_Igv_Descuento_Global;
                worksheet.Cells[totalRow + 4, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[totalRow + 4, 17].Style.Numberformat.Format = "0.00";



                worksheet.Cells[totalRow + 5, 14, totalRow + 5, 17].Merge = true;
                worksheet.Cells[totalRow + 5, 14].Value = "--------------------------------------------------------------------------";

                worksheet.Cells[totalRow + 6, 14].Value = "TOTAL:";
                worksheet.Cells[totalRow + 6, 15].Value = "USD";
                worksheet.Cells[totalRow + 6, 17].Value = sum_Usd_Neto + sum_Igv_Descuento_Global;
                worksheet.Cells[totalRow + 6, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[totalRow + 6, 17].Style.Numberformat.Format = "0.00";


                worksheet.Cells[totalRow + 6, 14].Style.Font.Bold = true;
                worksheet.Cells[totalRow + 6, 15].Style.Font.Bold = true;
                worksheet.Cells[totalRow + 6, 17].Style.Font.Bold = true;


                worksheet.Cells.AutoFitColumns();

                // Convertir el paquete de Excel a un array de bytes
                var fileBytes = package.GetAsByteArray();

                // Crear y devolver la respuesta
                ResponseFile response = new ResponseFile
                {
                    File = fileBytes,
                    Name = "Reporte_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx",
                    Type = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                };

                return response;
            }
        }
    }
}
