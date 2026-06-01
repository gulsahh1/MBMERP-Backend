using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;

namespace Core.Entities;

public class StockTransaction
{
    public int StockTransactionID { get; set; }
    public int ProductID { get; set; }
    public Product? Product { get; set; }
    public decimal Quantity { get; set; } // + giriş, - çıkış
    public TransactionType TransactionType { get; set; } 
    public DateTime Date { get; set; } = DateTime.Now;
    public bool IsActive { get; set; } = true;
}
