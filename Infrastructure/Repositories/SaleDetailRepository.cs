using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SaleDetailRepository : ISaleDetailRepository
    {
        private readonly ERPDbContext _context;

        public SaleDetailRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SaleDetail saleDetail)
        {
            await _context.AddAsync(saleDetail);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SaleDetail>> GetAllAsync()
        {
            return await _context.SaleDetails.ToListAsync();
        }

        public async Task<SaleDetail> GetByIdAsync(int id)
        {
            return await _context.SaleDetails.FirstOrDefaultAsync(x=>x.SaleDetailID == id);
        }

        public async Task<List<SaleDetail>> GetBySaleIdAsync(int saleID)
        {
            return await _context.SaleDetails.Include(x=>x.Product).Where(y=>y.SaleID == saleID && y.IsActive).ToListAsync();
        }

        public async Task UpdateAsync(SaleDetail saleDetail)
        {
            _context.SaleDetails.Update(saleDetail);
            await _context.SaveChangesAsync();
        }
    }
}
