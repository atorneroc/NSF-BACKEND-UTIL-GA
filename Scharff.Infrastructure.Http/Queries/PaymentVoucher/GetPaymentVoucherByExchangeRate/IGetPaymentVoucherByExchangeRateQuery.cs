namespace Scharff.Infrastructure.Http.Queries.PaymentVoucher.GetPaymentVoucherByExchangeRate
{
    public interface IGetPaymentVoucherByExchangeRateQuery
    {
        Task<int> GetPaymentVoucherByExchangeRate(string issue_date);
    }
}
