using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Commands.Stocks;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using MediatR;

namespace Application.Features.Commands.StockTransactions
{
    public class CreateStockTransactionCommand :IRequest<int>
    {
        public int ProductID { get; set; }
        public decimal Quantity { get; set; } 
        public TransactionType TransactionType { get; set; }
    }
    public class CreateStockTransactionCommandHandler : IRequestHandler<CreateStockTransactionCommand, int>
    {
        private readonly IStockTransactionRepository _stockTransactionRepository;
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateStockTransactionCommandHandler(
            IStockTransactionRepository stockTransactionRepository,
            IProductRepository productRepository, 
            IStockRepository stockRepository,
            IUnitOfWork unitOfWork)
        {
            _stockTransactionRepository = stockTransactionRepository;
            _productRepository = productRepository;
            _stockRepository = stockRepository;
            _unitOfWork = unitOfWork;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateStockTransactionCommand request, CancellationToken cancellationToken)
        {

            var product = await _productRepository.GetByIdAsync(request.ProductID);

            if (product == null)
                throw new Exception("Ürün bulunumadı");

            var stock = await _stockRepository.GetByProductIdAsync(request.ProductID);

            if (stock == null)
            {
                stock = new Stock
                {
                    ProductID = request.ProductID,
                    Quantity = 0,
                    UpdateDate = DateTime.Now
                };

                await _stockRepository.AddAsync(stock);
            }

            // NEGATIF STOK KONTROLÜ
            if (request.TransactionType == TransactionType.Sale &&
                stock.Quantity < request.Quantity)
            {
                throw new Exception("Yetersiz stok");
            }

            // transaction ekle

            var transaction = new StockTransaction
            {
                ProductID = request.ProductID,
                Quantity = request.Quantity,
                TransactionType = request.TransactionType
            };

             await _stockTransactionRepository.AddAsync(transaction);

            //stock güncelle

            switch (request.TransactionType)
            {
                case TransactionType.Purchase:
                    stock.Quantity += request.Quantity;
                    break;

                case TransactionType.Sale:
                    stock.Quantity -= request.Quantity;
                    break;

                case TransactionType.Adjustment:
                    stock.Quantity = request.Quantity;
                    break;
            }

            stock.UpdateDate = DateTime.Now;

            await _stockRepository.UpdateAsync(stock);
            try
            {
                await _unitOfWork.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw new Exception("Stok başka bir kullanıcı tarafından güncellendi.");
            }
            return transaction.StockTransactionID;
        }
    }
}
