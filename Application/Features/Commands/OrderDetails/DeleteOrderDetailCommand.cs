using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.OrderDetails
{
    public class DeleteOrderDetailCommand : IRequest<Unit>
    {
        public int OrderDetailID { get; set; }
    }

    public class DeleteOrderDetailCommandHandler : IRequestHandler<DeleteOrderDetailCommand, Unit>
        {
            private readonly IOrderDetailRepository _orderDetailRepository;
            public DeleteOrderDetailCommandHandler(IOrderDetailRepository orderDetailRepository)
            {
                _orderDetailRepository = orderDetailRepository;
            }
            public async Task<Unit> Handle(DeleteOrderDetailCommand request, CancellationToken cancellationToken)
            {
                var orderDetail = await _orderDetailRepository.GetByIdAsync(request.OrderDetailID);
                if (orderDetail == null)
                    throw new Exception("Sipariş Detayı Bulunamadı");

                orderDetail.IsActive = false;

               await _orderDetailRepository.UpdateAsync(orderDetail);
                return Unit.Value;
            }
    }

}
