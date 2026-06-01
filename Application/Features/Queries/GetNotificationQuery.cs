using Core.Entities;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries
{
    public class GetNotificationQuery: IRequest<List<Notification>>
    {
    }

    public class GetNotificationQueryHandler : IRequestHandler<GetNotificationQuery, List<Notification>>
    {
        private readonly INotificationRepository _notificationRepository;

        public GetNotificationQueryHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<List<Notification>> Handle(GetNotificationQuery request, CancellationToken cancellationToken)
        {
            var notification = await _notificationRepository.GetNotificationsAsync();



            return notification;

        }
    }
}
