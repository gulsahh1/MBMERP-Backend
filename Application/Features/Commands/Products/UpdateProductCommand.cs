using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using MediatR;

namespace Application.Features.Commands.Products
{
    public class UpdateProductCommand : IRequest<Unit>
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public decimal? CostPrice { get; set; }
        public string Unit { get; set; }
        public int StockQuantity { get; set; } = 0;
        public int CategoryID { get; set; }
    }

    public class UpdateProductCommandHandler :IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IDashboardHubService _dashboardHubService;

        public UpdateProductCommandHandler(IProductRepository productRepository, IDashboardHubService dashboardHubService)
        {
            _productRepository = productRepository;
            _dashboardHubService = dashboardHubService;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductID);

            if (product == null)
                throw new Exception("Ürün Bulunumadı");

            product.ProductName = request.ProductName;
            product.Price = request.Price;
            product.Description = request.Description;
            product.CostPrice = request.CostPrice;
            product.Unit= request.Unit;
            product.StockQuantity = request.StockQuantity;
            product.CategoryID = request.CategoryID;

            await _productRepository.UpdateAsync(product);


            return Unit.Value;
        }
    }
}
