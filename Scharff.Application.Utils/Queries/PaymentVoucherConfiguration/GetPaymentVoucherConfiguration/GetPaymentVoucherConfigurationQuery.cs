using MediatR;
using Scharff.Domain.Response.PaymentVoucherConfiguration;

namespace Scharff.Application.Queries.PaymentVoucherConfiguration.GetPaymentVoucherConfiguration
{
    public class GetPaymentVoucherConfigurationQuery : IRequest<ResponsePaymentVoucherConfiguration>
    {
        public int Id_Payment_Voucher_Type { get; set; }
    }
}
