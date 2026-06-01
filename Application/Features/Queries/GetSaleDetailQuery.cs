using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using MediatR;

namespace Application.Features.Queries
{
    public class GetSaleDetailQuery :IRequest<List<SaleDetail>>
    {
    }
    public class GetSaleDetailQueryHandler : IRequestHandler<GetSaleDetailQuery, List<SaleDetail>>
    {
        private readonly ISaleDetailRepository _saleDetailRepository;

        public GetSaleDetailQueryHandler(ISaleDetailRepository saleDetailRepository)
        {
            _saleDetailRepository = saleDetailRepository;
        }

        public async Task<List<SaleDetail>> Handle(GetSaleDetailQuery request, CancellationToken cancellationToken)
        {
            var saleDetail = await _saleDetailRepository.GetAllAsync();
            return saleDetail.Where(x => x.IsActive).ToList();

        }
    }
}
