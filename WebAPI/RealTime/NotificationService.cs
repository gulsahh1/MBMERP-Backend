using Application.Interfaces;
using Core.Enums;
using Core.Interfaces;
using Microsoft.AspNetCore.SignalR;
using WebAPI.Hubs;

namespace WebAPI.RealTime
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(IHubContext<NotificationHub> hubContext , INotificationRepository notificationRepository)
        {
            _hubContext = hubContext;
            _notificationRepository = notificationRepository;
        }

        public async Task CreateAsync(string title, string message, NotificationType type)
        {
            await _notificationRepository.AddNotificationAsync(title, message, type);

            await _hubContext.Clients.All.SendAsync("ReceiveNotification", new
            {
                Title = title,
                Message = message,
                Type = type.ToString()
            });
        }
    }
}
