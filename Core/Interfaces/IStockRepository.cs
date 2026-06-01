using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IStockRepository
    {
        Task AddAsync(Stock stock);
        Task UpdateAsync(Stock stock);
        Task<Stock> GetByIdAsync(int id);
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetByProductIdAsync(int productId);

    }
}
