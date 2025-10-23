namespace Scharff.Domain.Response.ExchangeRate.GetExchangeRateBroadCast
{
    public class ResponseGetExchangeRateBroadCast
    {
        public int id { get; set; }
        public DateTime? date_change { get; set; }
        public decimal certificate_purchase { get; set; }
        public decimal certificate_sale { get; set; }
        public decimal bank_purchase { get; set; }
        public decimal bank_sale { get; set; }
        public decimal parallel_purchase { get; set; }
        public decimal parallel_sale { get; set; }
        public DateTime creation_date { get; set; }
        public string? creation_author { get; set; } = string.Empty;
        public DateTime? modification_date { get; set; }
        public string? modification_author { get; set; } = string.Empty;
    }
}
