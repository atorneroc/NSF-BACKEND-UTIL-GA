using Scharff.Domain.Response.ExchangeRate.GetAllExchangeRate;
using Scharff.Domain.Response.ExchangeRate.GetExchangeRateById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Infrastructure.PostgreSQL.Queries.ExchangeRate.GetExchangeRateById
{
    public interface IGetExchangeRateByIdQuery
    {
        Task<ResponseGetExchangeRateById> GetExchangeRateById(int id);
    }
}
