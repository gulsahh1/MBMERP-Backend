using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS
{
    public class DashboardDto
    {
        //KPI
        public int TotalProducts { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalOrders { get; set; }

        // GRAPH 1
        public List<DailySalesDto> DailySales { get; set; } = new();

        // GRAPH 2
        public List<OrderTrendDto> OrderTrends { get; set; } = new();

        // GRAPH 3
        public List<StockMovmentDto> StockMovements { get; set; } = new();

        // GRAPH 4
        public List<CategorySalesDto> CategorySales { get; set; } = new(); 

        // ANALYTICS
        public List<TopProductDto> TopProducts { get; set; } = new();

    }
}
