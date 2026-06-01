using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Commands.Sales;
using Core.Entities;
using Core.Interfaces;
using MediatR;

namespace Application.Features.Commands.Stocks
{
    public class CreateStockCommand :IRequest<int>
    {
        public int ProductID { get; set; }
        public decimal Quantity { get; set; } 
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }

    public class CreateStockCommandHandler : IRequestHandler<CreateStockCommand, int>
    {
        private readonly IStockRepository _stockRepository;

        public CreateStockCommandHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<int> Handle(CreateStockCommand request, CancellationToken cancellationToken)
        {
            var stock = new Stock
            {
               ProductID = request.ProductID,
               Quantity = request.Quantity,
               UpdateDate = request.UpdateDate,
            };

            await _stockRepository.AddAsync(stock);
            return stock.StockID;
        }
    }
}
