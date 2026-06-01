using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Commands.Sales;
using Core.Entities;
using Core.Interfaces;
using MediatR;

namespace Application.Features.Commands.SaleDetails
{
    public class CreateSaleDetailCommand :IRequest<int>
    {
        public int SaleID { get; set; }
        public int ProductID { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
    public class CreateSaleDetailCommandHandler : IRequestHandler<CreateSaleDetailCommand, int>
    {
        private readonly ISaleDetailRepository _saleDetailRepository;

        public CreateSaleDetailCommandHandler(ISaleDetailRepository saleDetailRepository)
        {
            _saleDetailRepository = saleDetailRepository;
        }

        public async Task<int> Handle(CreateSaleDetailCommand request, CancellationToken cancellationToken)
        {
            var saleDetail = new SaleDetail
            {
              SaleID = request.SaleID,
              ProductID = request.ProductID,
              Quantity = request.Quantity,
              UnitPrice = request.UnitPrice,
            };

            await _saleDetailRepository.AddAsync(saleDetail);
            return saleDetail.SaleDetailID;
        }
    }
}
