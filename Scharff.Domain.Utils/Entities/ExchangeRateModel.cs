using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Domain.Entities
{
    public class ExchangeRateModel
    {
        public int id { get; set; }
        public DateTime change_date { get; set; }
        public decimal bank_purchase { get; set; }
        public decimal bank_sale { get; set; }
        public DateTime creation_date { get; set; }
        public string creation_author { get; set; } = string.Empty;
        public DateTime? modification_date { get; set; }
        public string? modification_author { get; set; } = string.Empty;
    }
}
