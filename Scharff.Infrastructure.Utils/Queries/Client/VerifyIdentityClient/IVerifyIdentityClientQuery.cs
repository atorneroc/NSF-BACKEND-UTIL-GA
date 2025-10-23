using Scharff.Domain.Entities;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Client.VerifyIdentityClient
{
    public interface IVerifyIdentityClientQuery
    {
        Task<bool> VerifyIdentityClient(int documentTypeId, string? numberDocumentIdentity);
    }
}
