namespace Scharff.Domain.Response.Parameter.GetParameterByGroupId
{
    public class ResponseGetParameterByGroupId
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public int attribute_length { get; set; }
        public string correlational_code { get; set; } = string.Empty;
        public string tci_code { get; set; } = string.Empty;
        public string sap_code { get; set; } = string.Empty; 
        public string detail_code { get; set; } = string.Empty;
        public string? short_name { get; set; } = string.Empty;
        public string? code { get; set; } = string.Empty;
        public string? full_name { get; set; } = string.Empty;
        public decimal min_val { get; set; }
        public decimal max_val { get; set; }
    }
}

