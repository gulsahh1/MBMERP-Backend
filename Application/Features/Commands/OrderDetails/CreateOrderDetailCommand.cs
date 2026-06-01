using Core.Entities;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.OrderDetails
{
    public class CreateOrderDetailCommand : IRequest<int>
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class CreateOrderDetailCommandHandler : IRequestHandler<CreateOrderDetailCommand, int>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        public CreateOrderDetailCommandHandler(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }
        public async Task<int> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var orderDetail = new OrderDetail
            {
                OrderID = request.OrderID,
                ProductID = request.ProductID,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice,
                TotalPrice = request.TotalPrice
            };
            await _orderDetailRepository.AddAsync(orderDetail);
            return orderDetail.OrderDetailID;
        }
    }
 }
