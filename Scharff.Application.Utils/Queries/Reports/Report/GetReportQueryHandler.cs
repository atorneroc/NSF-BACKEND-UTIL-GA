using MediatR;
using IronPdf;
using IronPdf.Rendering;
using Scharff.Infrastructure.PostgreSQL.Queries.Reports.Report;

namespace Scharff.Application.Queries.Reports.Report
{
    public class GetReportQueryHandler : IRequestHandler<GetReportQuery, Stream>
    {
        private readonly IGetReportQuery _getReportQuery;

        public GetReportQueryHandler(IGetReportQuery getReportQuery)
        {
            _getReportQuery = getReportQuery;
        }

        public async Task<Stream> Handle(GetReportQuery request, CancellationToken cancellationToken)
        {
            var result = await _getReportQuery.GetReport();

            string html = File.ReadAllText("Utils/ReportTemplate/ReportTemplate.html");
            var filas = "";

            result.ForEach(x =>
            {
                filas += $@" <tr>
                                <th class='col-1'>{x.Company_Name}</th>
                                <th class='col-2'>{x.Branch_Office_Name}</th>
                                <th class='col-3'>{x.Business_Unit_Name}</th>
                                <th class='col-4'>{x.Store_Name}</th>
                                <th class='col-5'>{x.Status_Description}</th>
                                <th class='col-6'>{x.Currency_Type} {x.Subtotal_Price}</th>
                             </tr>";
            });

            html = html.Replace("[DATA]", filas);

            var renderer = new HtmlToPdf();
            renderer.RenderingOptions.PaperOrientation = PdfPaperOrientation.Landscape;
            renderer.RenderingOptions.PaperSize = PdfPaperSize.A2;
            renderer.RenderingOptions.MarginBottom = 0;
            renderer.RenderingOptions.MarginTop = 0;
            renderer.RenderingOptions.MarginLeft = 0;
            renderer.RenderingOptions.MarginRight = 0;

            var document = renderer.RenderHtmlAsPdf(html);

            return document.Stream;
        }
    }
}
