using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public Order? Order { get; set; }
        public int ProductID { get; set; }
        public Product? Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
