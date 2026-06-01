using Application.Interfaces;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Products
{
    public class CreateProductCommand : IRequest<int>
    {
        public string ProductName { get; set; } 
        public decimal Price { get; set; } 
        public string? Description { get; set; } 
        public decimal? CostPrice { get; set; } 
        public string Unit { get; set; }
        public int StockQuantity { get; set; } = 0;
        public int CategoryID { get; set; }
    }

    public class  CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductRepository _productRepository;
        private readonly INotificationService _notificationService;

        public CreateProductCommandHandler(IProductRepository productRepository, INotificationService notificationService)
        {
            _productRepository = productRepository;
            _notificationService = notificationService;

        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                ProductName = request.ProductName,
                Price = request.Price,
                Description = request.Description,
                CostPrice = request.CostPrice,
                Unit = request.Unit,
                StockQuantity = request.StockQuantity,
                CategoryID = request.CategoryID,
            
            };


            await _productRepository.AddAsync(product);

            await _notificationService.CreateAsync(
                           "Yeni Ürün",
                            $"{product.ProductName} eklendi",
                             NotificationType.ProductCreated
                             );
  
        
            return product.ProductID;
        }
    }
}
