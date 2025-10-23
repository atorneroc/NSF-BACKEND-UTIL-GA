using MediatR;
using Scharff.Domain.Response.Parameter.GetAssignmentCustomerByDocumentId;

namespace Scharff.Application.Queries.Parameter.GetAssignmentCustomerByDocumentId
{
    public class GetAssignmentCustomerByDocumentIdQuery : IRequest<List<ResponseGetAssignmentCustomerByDocumentId>>
    {
        public int DocumentTypeId { get; set; }
        public int ReceiptTypeId { get; set; }
        

    }
}
