namespace Scharff.Domain.Response.Parameter.GetAssignmentCustomerByDocumentId
{
    public class ResponseGetAssignmentCustomerByDocumentId
    {
        public int Id { get; set; }
        public int DocumentTypeId { get; set; }
        public string? DocumentDescription { get; set; }
        public int OperationTypeId { get; set; }
        public string? OperationDescription { get; set; }
        public string? OperationCode { get; set; }
        public int? AffectationTypeId { get; set; }
        public string? AffectationDescription { get; set; }
        public bool? IsMain { get; set; }
        public bool Status { get; set; }
    }
}
