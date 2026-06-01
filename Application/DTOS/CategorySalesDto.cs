using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS
{
    public class CategorySalesDto
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        //  toplam satış cirosu
        public decimal TotalRevenue { get; set; }

        //  satılan ürün miktarı
        public decimal TotalQuantity { get; set; }

        // pie / donut için
        public decimal Percentage { get; set; }
    }
}
