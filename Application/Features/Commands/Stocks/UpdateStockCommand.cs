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
    public class UpdateStockCommand : IRequest<Unit>
    {
        public int StockID { get; set; }
        public int ProductID { get; set; }
        public decimal Quantity { get; set; }
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
    public class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand, Unit>
    {
        private readonly IStockRepository _stockRepository;

        public UpdateStockCommandHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<Unit> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
        {
            var stock = await _stockRepository.GetByIdAsync(request.StockID);

            if (stock == null)
                throw new Exception("Stok Kaydı Bulunamadı");

            stock.Quantity = request.Quantity;
            stock.UpdateDate = request.UpdateDate;
            stock.ProductID = request.ProductID;

            await _stockRepository.UpdateAsync(stock);

            return Unit.Value;
        }
    }
}
