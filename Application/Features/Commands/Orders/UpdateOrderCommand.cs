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
    public class UpdateOrderCommand : IRequest<Unit>
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public string OrderNumber { get; set; }
        public string? Description { get; set; }
        public int CustomerID { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand , Unit>
    {
        private IOrderRepository _IOderRepository;

        public UpdateOrderCommandHandler(IOrderRepository ıOderRepository)
        {
            _IOderRepository = ıOderRepository;
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _IOderRepository.GetByIdAsync(request.OrderID);

            if (order == null)
                throw new Exception("Sipariş Bulunamadı");

            order.OrderDate = request.OrderDate;
            order.TotalAmount = request.TotalAmount;
            order.OrderNumber = request.OrderNumber;
            order.Description = request.Description;
            order.CustomerID = request.CustomerID;
            order.OrderStatus = request.OrderStatus;

            await _IOderRepository.UpdateAsync(order);
            return Unit.Value;
        }
    }
}
