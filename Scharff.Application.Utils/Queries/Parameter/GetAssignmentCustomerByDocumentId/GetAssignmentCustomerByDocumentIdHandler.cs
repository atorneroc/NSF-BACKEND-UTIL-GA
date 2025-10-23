using MediatR;
using Scharff.Domain.Response.Parameter.GetAssignmentCustomerByDocumentId;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetAssignmentCustomerByDocumentId;
using System.ComponentModel.DataAnnotations;

namespace Scharff.Application.Queries.Parameter.GetAssignmentCustomerByDocumentId
{
    public class GetAssignmentCustomerByDocumentIdHandler : IRequestHandler<GetAssignmentCustomerByDocumentIdQuery, List<ResponseGetAssignmentCustomerByDocumentId>>
    {
        private readonly IGetAssignmentCustomerByDocumentIdQuery _getAssignmentCustomerByDocumentId;

        public GetAssignmentCustomerByDocumentIdHandler(IGetAssignmentCustomerByDocumentIdQuery getAssignmentCustomerByDocumentId)
        {
            _getAssignmentCustomerByDocumentId = getAssignmentCustomerByDocumentId;
        }

        public async Task<List<ResponseGetAssignmentCustomerByDocumentId>> Handle(GetAssignmentCustomerByDocumentIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _getAssignmentCustomerByDocumentId.GetAssignmentCustomerByDocumentIdAsync(request.DocumentTypeId,request.ReceiptTypeId);

            if (result == null || result.Count == 0)
            {
                throw new ValidationException($"Nos se encontraron resultados de tipo de operación para el documento.");
            }

            return result;
        }
    }
}