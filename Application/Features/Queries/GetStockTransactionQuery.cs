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
    public class GetStockTransactionQuery :IRequest<List<StockTransaction>>
    {
    }
    public class GetStockTransactionQueryHandler : IRequestHandler<GetStockTransactionQuery, List<StockTransaction>>
    {
        private readonly IStockTransactionRepository _stockTransactionRepository;

        public GetStockTransactionQueryHandler(IStockTransactionRepository stockTransactionRepository)
        {
            _stockTransactionRepository = stockTransactionRepository;
        }

        public async Task<List<StockTransaction>> Handle(GetStockTransactionQuery request, CancellationToken cancellationToken)
        {
            var stockTransaction = await _stockTransactionRepository.GetAllAsync();
            return stockTransaction.Where(x => x.IsActive).ToList();

        }
    }
}
