namespace Scharff.Domain.Response.Parameter.ValidateConfiguredService
{
    public class ConfiguredServiceResponse
    {
        public int Id { get; set; }
        public int Id_Operational_Structure_Config { get; set; }
        public int Id_Product { get; set; }
        public string? Product_Detail { get; set; }
        public int Id_Billing_Type { get; set; }
        public string? Billing_Type_Detail { get; set; }
        public int Id_Expense_Income_Type { get; set; }
        public string? Expense_Income_Type_Detail { get; set; }
        public int Id_Organizational_Service_Structure { get; set; }
    }
}