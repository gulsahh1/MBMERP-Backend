using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities;

public class Sale
{
    public int SaleID { get; set; }
    public int CustomerID { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalAmount { get; set; }
    public  List<SaleDetail> SaleDetails { get; set; }
    public bool IsActive { get; set; } = true;

}
