namespace Scharff.Domain.Response.Ubigeo.CheckUbigeoBySapCodes
{
    public class ResponseUbigeoLocationBySapCodes
    {
        public int Id { get; set; }
        public string UbigeoCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string SapCountryCode { get; set; } = string.Empty;
        public string SapRegionCode { get; set; } = string.Empty;
    }
}
