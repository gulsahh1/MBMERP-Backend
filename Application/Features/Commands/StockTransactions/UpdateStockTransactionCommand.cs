using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Commands.Stocks;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using MediatR;

namespace Application.Features.Commands.StockTransactions
{
    public class UpdateStockTransactionCommand :IRequest<Unit>
    {
        public int StockTransactionID { get; set; }
        public int ProductID { get; set; }
        public decimal Quantity { get; set; } 
        public TransactionType TransactionType { get; set; }
    }
    public class UpdateStockTransactionCommandHandler : IRequestHandler<UpdateStockTransactionCommand, Unit>
    {
        private readonly IStockTransactionRepository _stockTransactionRepository;

        public UpdateStockTransactionCommandHandler(IStockTransactionRepository stockTransactionRepository)
        {
            _stockTransactionRepository = stockTransactionRepository;
        }

        public async Task<Unit> Handle(UpdateStockTransactionCommand request, CancellationToken cancellationToken)
        {
            var stockTransaction = await _stockTransactionRepository.GetByIdAsync(request.StockTransactionID);

            if (stockTransaction == null)
                throw new Exception("Stok Kaydı Bulunamadı");

            stockTransaction.Quantity = request.Quantity;
            stockTransaction.ProductID = request.ProductID;
            stockTransaction.TransactionType = request.TransactionType;

            await _stockTransactionRepository.UpdateAsync(stockTransaction);

            return Unit.Value;
        }
    }
}
