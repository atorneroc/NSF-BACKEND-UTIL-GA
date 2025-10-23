namespace Scharff.Domain.Response.Parameter.ValidateConfiguredServiceFree
{
    public class ConfiguredServiceFreeResponse
    {
        public int Id_Organizational_Service_Structure { get; set; }
        public int Id_Service { get; set; }
        public string? Description { get; set; }
        public string? Code_Integration { get; set; }
        public int Id_Type_Affectation { get; set; }
        public string? Type_Affectation_Description { get; set; }
        public int Id_Free_Type_Affectation { get; set; }
        public string? Free_Type_Affectation_Description { get; set; }
    }
}