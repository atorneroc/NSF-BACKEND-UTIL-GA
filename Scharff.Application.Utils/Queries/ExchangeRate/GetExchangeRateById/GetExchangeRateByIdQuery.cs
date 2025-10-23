using MediatR;
using Scharff.Domain.Response.ExchangeRate.GetExchangeRateById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Application.Queries.ExchangeRate.GetExchangeRateById
{
    public class GetExchangeRateByIdQuery : IRequest<ResponseGetExchangeRateById>
    {
        public int id { get; set; }
    }
}
