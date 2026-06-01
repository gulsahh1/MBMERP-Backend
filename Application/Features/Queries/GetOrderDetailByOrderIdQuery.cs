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
    public class GetOrderDetailByOrderIdQuery : IRequest<List<OrderDetail>>
    {
        public int OrderID { get; set; }
    }
    public class GetOrderDetailByOrderIdQueryHandler : IRequestHandler<GetOrderDetailByOrderIdQuery, List<OrderDetail>>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        public GetOrderDetailByOrderIdQueryHandler(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }
        public async Task<List<OrderDetail>> Handle(GetOrderDetailByOrderIdQuery request, CancellationToken cancellationToken)
        {
           
              var orderDetail =  await _orderDetailRepository.GetByOrderIdAsync(request.OrderID);
             return  orderDetail.Where(x=>x.IsActive).ToList();
        }

    }
}
