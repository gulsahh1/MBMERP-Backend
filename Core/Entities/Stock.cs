using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities;

public class Stock
{
    public int StockID { get; set; }
    public int ProductID { get; set; }
    public Product? Product { get; set; }
    public decimal Quantity { get; set; } // Miktar
    public DateTime UpdateDate { get; set; }
    public bool IsActive { get; set; } = true;
    [Timestamp]
    public byte[] RowVersion { get; set; }
}
