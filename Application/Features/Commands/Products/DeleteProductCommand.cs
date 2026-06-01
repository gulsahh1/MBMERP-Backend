using Application.Interfaces;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Products
{
    public class DeleteProductCommand: IRequest<Unit>
    {
        public int ProductID { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IDashboardHubService _dashboardHubService;
        private readonly IDashboardService _dashboardService;

        public DeleteProductCommandHandler(IProductRepository productRepository, IDashboardHubService dashboardHubService, IDashboardService dashboardService)
        {
            _productRepository = productRepository;
            _dashboardHubService = dashboardHubService;
            _dashboardService = dashboardService;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductID);

            if (product == null)
                throw new Exception("Silineceke Ürün Bulunamadı!");

            product.isActive = false;

            await _productRepository.UpdateAsync(product);

            await _dashboardHubService.RefreshDashboard();

            return Unit.Value;
        }
    }
}
