using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetValidToken(string token, int userId);
        Task AddAsync(RefreshToken token);
        Task SaveChangesAsync();
    }
}
