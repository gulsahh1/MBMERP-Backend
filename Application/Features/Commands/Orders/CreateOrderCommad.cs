using Application.Interfaces;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Orders
{
    public class CreateOrderCommad :IRequest<int>
    {
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public string OrderNumber { get; set; }
        public string? Description { get; set; }
       public int CustomerID { get; set; }
        public OrderStatus OrderStatus { get; set; }

    }

    public class CreateOrderCommadHandler  :IRequestHandler<CreateOrderCommad, int>
    {
        private readonly IOrderRepository _IOrderRepository;
        private readonly INotificationService _notificationService;
        public CreateOrderCommadHandler(IOrderRepository IOrderRepository, INotificationService notificationService)
        {
            _IOrderRepository = IOrderRepository;
            _notificationService = notificationService;
        }
        public async Task<int> Handle(CreateOrderCommad request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                OrderDate = request.OrderDate,
                TotalAmount = request.TotalAmount,
                OrderNumber = request.OrderNumber,
                Description = request.Description,
                CustomerID = request.CustomerID,
                OrderStatus = request.OrderStatus,
            };
            await _IOrderRepository.AddAsync(order);
            await _notificationService.CreateAsync(
                          "Yeni Sipariş",
                          $"{order.OrderNumber} numaralı sipariş oluşturuldu",
                           NotificationType.OrderCreated
                 );


            return order.OrderID;
        }
    }
}
