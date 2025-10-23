using MediatR;
using Scharff.Domain.Request.Report;
using Scharff.Domain.Response.Reports.GetReportInvoiceServiceByIdTypeServicesAndDateRangeQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Application.Queries.Reports.GenerateReportServiceOrderCourier
{
    public class GetReportServiceOrderCourierQuery : RequestReportServiceOrderCourier, IRequest<ResponseFile>
    {

    }
}
