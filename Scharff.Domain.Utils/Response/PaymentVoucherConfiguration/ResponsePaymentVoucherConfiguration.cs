using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Domain.Response.PaymentVoucherConfiguration
{
    public class ResponsePaymentVoucherConfiguration
    {
        public int Id { get; set; }
        public int Id_Payment_Voucher_Type { get; set; }
        public int Number_Days_Creation { get; set; }
        public int Number_Days_Cancellation { get; set; }
    }
}
