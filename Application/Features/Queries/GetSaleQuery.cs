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
    public class GetSaleQuery :IRequest<List<Sale>>
    {
    }

    public class GetSaleQueryHandler : IRequestHandler<GetSaleQuery, List<Sale>>
    {
        private readonly ISaleRepository _saleRepository;

        public GetSaleQueryHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<List<Sale>> Handle(GetSaleQuery request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetAllAsync();
            return sale.Where(x => x.IsActive).ToList();

        }
    }
}
