using Core.Entities;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface INotificationRepository
    {
            Task AddNotificationAsync(string title, string message, NotificationType type);

            Task<List<Notification>> GetNotificationsAsync();

    }
}
