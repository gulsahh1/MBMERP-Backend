using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public Customer? Customer { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public string OrderNumber { get; set; }
        public OrderStatus OrderStatus { get; set; }
        // Pending, Approved, Shipped, Completed, Cancelled
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();


    }
}
