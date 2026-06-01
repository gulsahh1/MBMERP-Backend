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
    public class GetStockTransactionsByProductIdQuery :IRequest<List<StockTransaction>>
    {
        public int ProductID { get; set; }

    }

    public class GetStockTransactionsByProductIdQueryHandler : IRequestHandler<GetStockTransactionsByProductIdQuery, List<StockTransaction>>
    {
        private readonly IStockTransactionRepository _stockTransactionRepository;

        public GetStockTransactionsByProductIdQueryHandler(IStockTransactionRepository stockTransactionRepository)
        {
            _stockTransactionRepository = stockTransactionRepository;
        }

        public async Task<List<StockTransaction>> Handle(GetStockTransactionsByProductIdQuery request, CancellationToken cancellationToken)
        {
            return await _stockTransactionRepository.GetStockTransactionsByProductIdAsync(request.ProductID);
        }
    }
}
