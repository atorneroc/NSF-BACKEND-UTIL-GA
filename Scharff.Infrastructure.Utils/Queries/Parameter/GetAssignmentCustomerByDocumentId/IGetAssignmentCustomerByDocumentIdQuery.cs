using Scharff.Domain.Response.Parameter.GetAssignmentCustomerByDocumentId;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetAssignmentCustomerByDocumentId
{
    public interface IGetAssignmentCustomerByDocumentIdQuery
    {
        Task<List<ResponseGetAssignmentCustomerByDocumentId>> GetAssignmentCustomerByDocumentIdAsync(int documentTypeId, int receiptTypeId);
    }
}
