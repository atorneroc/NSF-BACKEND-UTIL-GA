namespace Scharff.Domain.Response.Ubigeo.GetAllCountryRegion
{
    public class ResponseGetAllCountryRegion
    {
        public int id { get; set; }
        public string? Ubigeo_Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;        
        public string Sap_Country_Code { get; set; } = string.Empty;
        public string Sap_Region_Code { get; set; } = string.Empty;
    }
}

