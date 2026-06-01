using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly ERPDbContext _context;
        public OrderDetailRepository(ERPDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(OrderDetail orderDetail)
        {
             await _context.OrderDetails.AddAsync(orderDetail);
             await _context.SaveChangesAsync();
        }

        public async Task<List<OrderDetail>> GetAllAsync()
        {
            return await _context.OrderDetails.ToListAsync();
        }

        public async Task<OrderDetail> GetByIdAsync(int id)
        {
            return await _context.OrderDetails.FirstOrDefaultAsync(x=>x.OrderDetailID == id);
        }

        public async Task<List<OrderDetail>> GetByOrderIdAsync(int orderID)
        {
            return await _context.OrderDetails.Where(x => x.OrderID == orderID).ToListAsync();
        }

        public async Task<List<OrderDetail>> GetByOrderIdWithProductAsync(int orderId)
        {
            return await _context.OrderDetails.Where(x => x.OrderID == orderId).Include(x => x.Product).ToListAsync();
        }

        public async Task<OrderDetail> GetWithProductAsync(int id)
        {
            return await _context.OrderDetails.Where(x => x.OrderDetailID == id).Include(x => x.Product).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(OrderDetail orderDetail)
        {
            _context.OrderDetails.Update(orderDetail);
            await _context.SaveChangesAsync();
        }
    }
}
