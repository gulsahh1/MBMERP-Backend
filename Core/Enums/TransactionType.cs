using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public enum TransactionType
    {
        Purchase = 1,   // Stok girişi
        Sale = 2,       // Stok çıkışı
        Adjustment = 3  // Manuel düzeltme
    }
}
