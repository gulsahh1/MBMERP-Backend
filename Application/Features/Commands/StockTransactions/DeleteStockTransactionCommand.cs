using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using MediatR;

namespace Application.Features.Commands.StockTransactions
{
    public class DeleteStockTransactionCommand :IRequest<Unit>
    {
        public int StockTransactionID { get; set; }
    }
    public class DeleteStockTransactionCommandHandler : IRequestHandler<DeleteStockTransactionCommand, Unit>
    {
        private readonly IStockTransactionRepository _stockTransactionRepository;

        public DeleteStockTransactionCommandHandler(IStockTransactionRepository stockTransactionRepository)
        {
            _stockTransactionRepository = stockTransactionRepository;
        }

        public async Task<Unit> Handle(DeleteStockTransactionCommand request, CancellationToken cancellationToken)
        {
            var stockTransaction = await _stockTransactionRepository.GetByIdAsync(request.StockTransactionID);

            if (stockTransaction == null)
                throw new Exception("Stok Kaydı Bulunamadı");

            stockTransaction.IsActive = false;

            await _stockTransactionRepository.UpdateAsync(stockTransaction);

            return Unit.Value;
        }
    }
}
