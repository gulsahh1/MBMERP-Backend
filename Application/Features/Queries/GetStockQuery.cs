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
    public class GetStockQuery :IRequest<List<Stock>>
    {
    }
    public class GetStockQueryHandler : IRequestHandler<GetStockQuery, List<Stock>>
    {
        private readonly IStockRepository _stockRepository;

        public GetStockQueryHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<List<Stock>> Handle(GetStockQuery request, CancellationToken cancellationToken)
        {
            var stock = await _stockRepository.GetAllAsync();
            return stock.Where(x => x.IsActive).ToList();

        }
    }
}
