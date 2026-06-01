using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using MediatR;

namespace Application.Features.Commands.Stocks
{
    public class DeleteStockCommand :IRequest<Unit>
    {
        public int StockID { get; set; }
    }

    public class DeleteStockCommandHandler : IRequestHandler<DeleteStockCommand, Unit>
    {
        private readonly IStockRepository _stockRepository;

        public DeleteStockCommandHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<Unit> Handle(DeleteStockCommand request, CancellationToken cancellationToken)
        {
            var stock = await _stockRepository.GetByIdAsync(request.StockID);

            if (stock == null)
                throw new Exception("Stok Kaydı Bulunamadı");

            stock.IsActive = false;

            await _stockRepository.UpdateAsync(stock);

            return Unit.Value;
        }
    }
}
