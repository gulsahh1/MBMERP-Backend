using Core.Entities;
using Core.Enums;
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
    public class NotificationRepository : INotificationRepository
    {
        private readonly ERPDbContext _context;

        public NotificationRepository(ERPDbContext context)
        {
            _context = context;
        }

        public async Task AddNotificationAsync(string title, string message, NotificationType type)
        {
             await _context.Notifications.AddAsync(new Notification
            {
                Title = title,
                Message = message,
                Type = type,
            });
             await _context.SaveChangesAsync();

        }

        public async Task<List<Notification>> GetNotificationsAsync()
        {
            return await _context.Notifications.AsNoTracking().OrderByDescending(x => x.CreatedDate).Take(50).ToListAsync();
        }
    }
}
