using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IOrderDetailRepository
    {
        Task AddAsync(OrderDetail orderDetail);
        Task UpdateAsync(OrderDetail orderDetail);
        Task<OrderDetail> GetByIdAsync(int id);
        Task<List<OrderDetail>> GetAllAsync();
        Task<List<OrderDetail>> GetByOrderIdAsync(int orderID);
        Task<OrderDetail> GetWithProductAsync(int id);
        Task<List<OrderDetail>> GetByOrderIdWithProductAsync(int orderId);
    }
}
