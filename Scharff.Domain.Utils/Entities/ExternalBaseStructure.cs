namespace Scharff.Domain.Entities
{
    public class ExternalBaseStructure
    {
        public string DocumentNumber { get; set; }
        public string BranchCodeIntOF { get; set; }
        public string BusinessUnitCodeIntOF { get; set; }
        public string ProductCodeIntOF { get; set; }
        public string ServiceCodeIntOF { get; set; }

        public int CompanyId { get; set; }
        public int ProductId { get; set; }
        public int BranchId { get; set; }
        public int BusinessUnitId { get; set; }
        public int ServiceId { get; set; }
    }
}
