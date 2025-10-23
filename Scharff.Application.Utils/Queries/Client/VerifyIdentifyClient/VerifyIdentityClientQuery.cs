using MediatR;
using Scharff.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Application.Queries.Client.VerifyIdentifyClient
{
    public class VerifyIdentityClientQuery : IRequest<bool>
    {
        public string? NumberDocumentIdentity { get; set; }
        public int documentTypeId { get; set; }
    }
}
