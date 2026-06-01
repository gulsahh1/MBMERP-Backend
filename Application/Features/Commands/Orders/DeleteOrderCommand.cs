using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Orders
{
    public class DeleteOrderCommand: IRequest<Unit>
     {
        public int OrderID { get; set; }
    }

    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;
        public DeleteOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderID);
            if (order == null)
                throw new Exception("Sipariş Bulunamadı");

            order.IsActive = false; 

            await _orderRepository.UpdateAsync(order);
            return Unit.Value;
        }
    }

}
