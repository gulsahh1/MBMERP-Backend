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
    public class GetOrderDetailQuery : IRequest<List<OrderDetail>>
    {
      
    }

    public class GetOrderDetailQueryHandler : IRequestHandler<GetOrderDetailQuery, List<OrderDetail>>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        public GetOrderDetailQueryHandler(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }
        public async Task<List<OrderDetail>> Handle(GetOrderDetailQuery request, CancellationToken cancellationToken)
        {
             var orderDetails = await _orderDetailRepository.GetAllAsync();
             return orderDetails.Where(x=>x.IsActive).ToList();
        }
    }
}
