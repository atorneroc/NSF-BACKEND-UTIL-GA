using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Domain.Response.ExchangeRate.GetAllExchangeRate
{
    public class ResponseGetAllExchangeRate
    {
        public int id { get; set; }
        public DateTime? date_change { get; set; }
        public decimal bank_purchase { get; set; }
        public decimal bank_sale { get; set; }
        public bool edit { get; set; }
    }
}
