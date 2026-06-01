using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;

namespace Core.Entities
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int CustomerID { get; set; }
        public Customer? Customer { get; set; }
        public decimal Amount { get; set; } // ödenen para
        public DateTime Date { get; set; }
        public PaymentType PaymentType { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
