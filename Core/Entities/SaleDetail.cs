using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities;

public class SaleDetail
{
    public int SaleDetailID { get; set; }
    public int SaleID { get; set; }
    public Sale? Sale { get; set; }
    public int ProductID { get; set; }
    public Product ? Product { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public bool IsActive { get; set; } = true;
}
