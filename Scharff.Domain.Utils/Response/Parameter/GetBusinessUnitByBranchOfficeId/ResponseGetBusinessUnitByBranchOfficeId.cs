namespace Scharff.Domain.Response.Parameter.GetBusinessUnitByBranchOfficeId
{
    public class ResponseGetBusinessUnitByBranchOfficeId
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string integration_code { get; set; } = string.Empty;
    }
}
