using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.OrderDetails
{
    public class UpdateOrderDetailCommand : IRequest<Unit>
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class UpdateOrderDetailCommandHandler : IRequestHandler<UpdateOrderDetailCommand, Unit>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        public UpdateOrderDetailCommandHandler(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }
        public async Task<Unit> Handle(UpdateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var orderDetail = await _orderDetailRepository.GetByIdAsync(request.OrderDetailID);
            if (orderDetail == null)
                throw new Exception("Sipariş Detayı Bulunamadı");

            orderDetail.OrderID = request.OrderID;
            orderDetail.ProductID = request.ProductID;
            orderDetail.Quantity = request.Quantity;
            orderDetail.UnitPrice = request.UnitPrice;
            orderDetail.TotalPrice = request.TotalPrice;

            await _orderDetailRepository.UpdateAsync(orderDetail);
            return Unit.Value;
        }
    }
}
