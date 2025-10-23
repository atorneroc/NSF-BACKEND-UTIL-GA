using Scharff.Domain.Response.Parameter.GetIgv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetIgv
{
    public interface IGetIgvQuery
    {
        Task<List<ResponseGetIgv>> GetIgv();
    }
}
