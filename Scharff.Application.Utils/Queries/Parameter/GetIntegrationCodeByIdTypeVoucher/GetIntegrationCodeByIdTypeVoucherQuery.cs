using MediatR;
using Scharff.Domain.Response.Parameter.GetIntegrationCodeByIdTypeVoucher;

namespace Scharff.Application.Queries.Parameter.GetIntegrationCodeByIdTypeVoucher
{
    public class GetIntegrationCodeByIdTypeVoucherQuery : IRequest<List<ResponseGetIntegrationCodeByIdTypeVoucher>>
    {
        public int idTypeVoucher { get; set; }
    }

}