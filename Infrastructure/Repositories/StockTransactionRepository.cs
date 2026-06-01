using System;
using System.Collections;
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
    public class StockTransactionRepository :IStockTransactionRepository
    {
        private readonly ERPDbContext _context;

        public StockTransactionRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(StockTransaction stockTransaction)
        {
            await _context.StockTransactions.AddAsync(stockTransaction);
        }

        public async Task<List<StockTransaction>> GetAllAsync()
        {
            return await _context.StockTransactions.ToListAsync();
        }

        public async Task<StockTransaction> GetByIdAsync(int id)
        {
            return await _context.StockTransactions.FirstOrDefaultAsync(x => x.StockTransactionID == id);
        }

        public async Task<List<StockTransaction>> GetStockTransactionsByProductIdAsync(int productID)
        {
            return await _context.StockTransactions.Where(x => x.ProductID == productID).ToListAsync();
        }

        public async Task UpdateAsync(StockTransaction stockTransaction)
        {
            _context.StockTransactions.Update(stockTransaction);
        }
    }
}
