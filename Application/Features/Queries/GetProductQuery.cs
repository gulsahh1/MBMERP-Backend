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
    public class GetProductQuery :IRequest<List<Product>>
    {
    }

    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, List<Product>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IDashboardHubService _dashboardHubService;


        public GetProductQueryHandler(IProductRepository productRepository, IDashboardHubService dashboardHubService)
        {
            _productRepository = productRepository;
            _dashboardHubService = dashboardHubService;
        }

        public async Task<List<Product>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetAllAsync();

            return product.Where(x => x.isActive).ToList();
        }
    }
}
