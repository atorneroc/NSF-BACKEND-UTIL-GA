namespace Scharff.Domain.Response.Ubigeo.GetUbigeoByCode
{
    public class ResponseGetUbigeoByCode
    {
        public int id { get; set; }
        public string? Ubigeo_Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Postal_Code { get; set; } = string.Empty; 
    }
}

