namespace Scharff.Domain.Response.Parameter.GetIntegrationCodeByIdTypeVoucher
{
    public class ResponseGetIntegrationCodeByIdTypeVoucher
    {
        public int id_type_voucher { get; set; }
        public string customer_integration_code { get; set; } = string.Empty;
        public bool state { get; set; }
    }
}
