using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ISaleRepository
    {
        Task AddAsync(Sale sale);
        Task UpdateAsync(Sale sale);
        Task<Sale> GetByIdAsync(int id);
        Task<List<Sale>> GetAllAsync();
    }
}
