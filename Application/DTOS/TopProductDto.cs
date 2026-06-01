using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS
{
    public class TopProductDto
    {
        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        //  kaç adet satıldı
        public decimal TotalSoldQuantity { get; set; }

        //  toplam ciro
        public decimal TotalRevenue { get; set; }

        //  maliyet vs satış farkı (çok değerli KPI)
        public decimal Profit { get; set; }

        public string? CategoryName { get; set; }

        public int Rank { get; set; }
    }
}
