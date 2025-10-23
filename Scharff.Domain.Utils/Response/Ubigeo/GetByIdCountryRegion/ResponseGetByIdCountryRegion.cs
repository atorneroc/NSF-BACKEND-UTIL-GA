namespace Scharff.Domain.Response.Ubigeo.GetByIdCountryRegion
{
    public class ResponseGetByIdCountryRegion
    {
        public int id { get; set; }
        public string? Ubigeo_Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Sap_Country_Code { get; set; } = string.Empty;
        public string Sap_Region_Code { get; set; } = string.Empty;
    }
}
