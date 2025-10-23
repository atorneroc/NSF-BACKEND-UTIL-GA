using MediatR;
using Scharff.Domain.Response.Parameter.GetStoreByBusinessUnitId;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Application.Queries.Parameter.GetStoreByBusinessUnitId
{
    public class GetStoreByBusinessUnitIdQuery : IRequest<List<ResponseGetStoreByBusinessUnitId>>
    {
        public int CompanyId { get; set; }
        public int branchOfficeId { get; set; }
        public int businessUnitId { get; set; }
    }
}