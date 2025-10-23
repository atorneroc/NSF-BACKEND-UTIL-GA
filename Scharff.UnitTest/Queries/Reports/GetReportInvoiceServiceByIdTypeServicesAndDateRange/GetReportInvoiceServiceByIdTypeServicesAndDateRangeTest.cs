using Microsoft.AspNetCore.Routing;
using Moq;
using Scharff.Application.Queries.Reports.ReportInvoiceServiceByIdTypeServicesAndDateRange;
using Scharff.Domain.Response.Reports.GetReportInvoiceServiceByIdTypeServicesAndDateRangeQuery;
using Scharff.Infrastructure.Http.Queries.Reports.GetReportInvoiceServiceByIdTypeServicesAndDateRange;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.UnitTest.Queries.Reports.GetReportInvoiceServiceByIdTypeServicesAndDateRange
{
    public class GetReportInvoiceServiceByIdTypeServicesAndDateRangeTest
    {
        [Fact]
        public async Task Handle_TREP1_ReturnsResponseFile()
        {
            // Arrange
            var mockService = new Mock<IGetReportInvoiceServiceByIdTypeServicesAndDateRange>();
            var handler = new GetReportInvoiceServiceByIdTypeServicesAndDateRangeHandler(mockService.Object);
            var request = new Application.Queries.Reports.ReportInvoiceServiceByIdTypeServicesAndDateRange.GetReportInvoiceServiceByIdTypeServicesAndDateRangeQuery
            {
                codTypeReport = "TREP1",
                issue_Date_Start = new DateTime(2024, 4, 1),
                issue_Date_End = new DateTime(2024, 4, 30)
            };

            var dataSet = new DataSet();
            var dtHeader = new DataTable();
            dtHeader.Columns.Add("HeaderColumn1");
            dtHeader.Rows.Add("HeaderData1");
            dataSet.Tables.Add(dtHeader);

            var dtBody = new DataTable();
            dtBody.Columns.Add("BodyColumn1");
            dtBody.Rows.Add("BodyData1");
            dataSet.Tables.Add(dtBody);

            mockService.Setup(s => s.GetReportfreight(It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(dataSet);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result is ResponseFile);
        }


        [Fact]
        public async Task Handle_TREP2_ReturnsResponseFile()
        {
            // Arrange
            var mockService = new Mock<IGetReportInvoiceServiceByIdTypeServicesAndDateRange>();
            var handler = new GetReportInvoiceServiceByIdTypeServicesAndDateRangeHandler(mockService.Object);
            var request = new Application.Queries.Reports.ReportInvoiceServiceByIdTypeServicesAndDateRange.GetReportInvoiceServiceByIdTypeServicesAndDateRangeQuery
            {
                codTypeReport = "TREP2",
                issue_Date_Start = new DateTime(2024, 4, 1),
                issue_Date_End = new DateTime(2024, 4, 30)
            };

            var dataSet = new DataSet();
            var dtHeader = new DataTable();
            dtHeader.Columns.Add("HeaderColumn1");
            dtHeader.Rows.Add("HeaderData1");
            dataSet.Tables.Add(dtHeader);

            var dtBody = new DataTable();
            dtBody.Columns.Add("BodyColumn1");
            dtBody.Rows.Add("BodyData1");
            dataSet.Tables.Add(dtBody);

            mockService.Setup(s => s.GetReportFee(It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(dataSet);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result is ResponseFile);
        }


        [Fact]
        public async Task Handle_TREP3_ReturnsResponseFile()
        {
            // Arrange
            var mockService = new Mock<IGetReportInvoiceServiceByIdTypeServicesAndDateRange>();
            var handler = new GetReportInvoiceServiceByIdTypeServicesAndDateRangeHandler(mockService.Object);
            var request = new Application.Queries.Reports.ReportInvoiceServiceByIdTypeServicesAndDateRange.GetReportInvoiceServiceByIdTypeServicesAndDateRangeQuery
            {
                codTypeReport = "TREP3",
                issue_Date_Start = new DateTime(2024, 4, 1),
                issue_Date_End = new DateTime(2024, 4, 30)
            };

            var dataSet = new DataSet();
            var dtHeader = new DataTable();
            dtHeader.Columns.Add("HeaderColumn1");
            dtHeader.Rows.Add("HeaderData1");
            dataSet.Tables.Add(dtHeader);

            var dtBody = new DataTable();
            dtBody.Columns.Add("BodyColumn1");
            dtBody.Rows.Add("BodyData1");
            dataSet.Tables.Add(dtBody);

            mockService.Setup(s => s.GetReportAcreditations(It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(dataSet);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result is ResponseFile);
        }
    }
}
