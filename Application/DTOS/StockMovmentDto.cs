using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS
{
    public class StockMovmentDto
    {
        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public string? CategoryName { get; set; }

        public decimal Quantity { get; set; }

        // In / Out
        public string? TransactionType { get; set; }

        public DateTime Date { get; set; }

        //  işlem sonrası stok durumu
        public int CurrentStock { get; set; }
    }
}
