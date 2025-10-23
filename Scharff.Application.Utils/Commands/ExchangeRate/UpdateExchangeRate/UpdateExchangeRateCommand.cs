using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Application.Commands.ExchangeRate.UpdateExchangeRate
{
    public class UpdateExchangeRateCommand : IRequest<int>
    {
        public int id { get; set; }
        public DateTime change_date { get; set; }
        public decimal bank_purchase { get; set; }
        public decimal bank_sale { get; set; }
        public string user { get; set; } = string.Empty;
    }
}
