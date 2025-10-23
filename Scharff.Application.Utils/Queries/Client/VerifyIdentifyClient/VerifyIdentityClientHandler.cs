using MediatR;
using Scharff.Domain.Entities;
using Scharff.Infrastructure.PostgreSQL.Queries.Client.VerifyIdentityClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Application.Queries.Client.VerifyIdentifyClient
{
    public class VerifyIdentityClientHandler : IRequestHandler<VerifyIdentityClientQuery, bool>
    {
        private readonly IVerifyIdentityClientQuery _verifyIdentityClientQuery;
        public VerifyIdentityClientHandler(IVerifyIdentityClientQuery verifyIdentityClientQuery)
        {
            _verifyIdentityClientQuery = verifyIdentityClientQuery;
        }
        public async Task<bool> Handle(VerifyIdentityClientQuery request, CancellationToken cancellationToken)
        {

            return await _verifyIdentityClientQuery.VerifyIdentityClient(request.documentTypeId, request.NumberDocumentIdentity);

        }
    }
}
