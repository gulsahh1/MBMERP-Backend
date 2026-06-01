using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities;

public class Product
{
    public int ProductID { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; } // Satış Fiyatı
    public string? Description { get; set; } =string.Empty;
    public decimal? CostPrice { get; set; } // Maliyet Fiyatı
    public string Unit { get; set; } = "Adet";
    public bool isActive { get; set; } = true;
    public int StockQuantity { get; set; } 
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    // Relation
    public int CategoryID { get; set; }
    public Category? Category { get; set; }



}
