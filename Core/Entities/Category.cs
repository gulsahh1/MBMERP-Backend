using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities;

public class Category
{
    public int CategoryID { get; set; }
    public string CategoryName { get; set; }= string.Empty;
    public bool isActive { get; set; } = true;
    public DateTime CreatedDate { get; set; }= DateTime.Now;

}
