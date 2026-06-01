using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IStockTransactionRepository
    {
        Task AddAsync(StockTransaction stockTransaction);
        Task UpdateAsync(StockTransaction stockTransaction);
        Task<StockTransaction> GetByIdAsync(int id);
        Task<List<StockTransaction>> GetAllAsync();
        Task<List<StockTransaction>> GetStockTransactionsByProductIdAsync(int productID);
    }
}
