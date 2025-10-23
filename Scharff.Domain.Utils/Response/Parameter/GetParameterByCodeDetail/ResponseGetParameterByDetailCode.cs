namespace Scharff.Domain.Response.Parameter.GetParameterByCodeDetail
{
    public class ResponseGetParameterByDetailCode
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Detail_Code { get; set; } = string.Empty;
        public string? General_Code { get; set; } = string.Empty;
        public string?  General_Description { get; set; } = string.Empty;
    }
}