using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS
{
    public class OrderTrendDto
    {
        public DateTime Date { get; set; }
        public int ThisWeekOrders { get; set; }
        public int LastWeekOrders { get; set; }
    }
}
