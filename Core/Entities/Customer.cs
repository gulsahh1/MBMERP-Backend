using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;

namespace Core.Entities;

public class Customer
{
    public int CustomerID { get; set; }
    public string CustomerName { get; set; }= string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public decimal Balance { get; set; } = 0;  // Borç-Alacak Durumu
    public bool isActive { get; set; } = true;
    public DateTime CreatedDate { get; set; }= DateTime.Now;
    public CustomerType CustomerType { get; set; }


}
