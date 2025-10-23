using Scharff.Domain.Response.PaymentVoucherConfiguration;

namespace Scharff.Infrastructure.PostgreSQL.Queries.PaymentVoucherConfiguration.GetPaymentVoucherConfiguration
{
    public interface IGetPaymentVoucherConfigurationQuery
    {
        Task<ResponsePaymentVoucherConfiguration> GetPaymentVoucherConfiguration(int Id_Payment_Voucher_Type);
    }
}
