using Scharff.Domain.Response.Parameter.GetStoreByBusinessUnitId;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetStoreByBusinessUnitId
{
    public interface IGetStoreByBusinessUnitIdQuery
    {
        Task<List<ResponseGetStoreByBusinessUnitId>> GetStoreByBusinessUnitId(int CompanyId, int branchOfficeId, int businessUnitId);
    }
}
