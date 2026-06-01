using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using MediatR;

namespace Application.Features.Commands.SaleDetails
{
    public class UpdateSaleDetailCommand :IRequest<Unit>
    {
        public int SaleDetailID { get; set; }
        public int SaleID { get; set; }
        public int ProductID { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class UpdateSaleDetailCommandHandler : IRequestHandler<UpdateSaleDetailCommand, Unit>
    {
        private readonly ISaleDetailRepository _saleDetailRepository;

        public UpdateSaleDetailCommandHandler(ISaleDetailRepository saleDetailRepository)
        {
            _saleDetailRepository = saleDetailRepository;
        }

        public async Task<Unit> Handle(UpdateSaleDetailCommand request, CancellationToken cancellationToken)
        {
            var saleDetail = await _saleDetailRepository.GetByIdAsync(request.SaleDetailID);

            if (saleDetail == null)
                throw new Exception("Kayıt Bulunamadı");

            saleDetail.Quantity = request.Quantity;
            saleDetail.UnitPrice = request.UnitPrice;
            saleDetail.SaleID = request.SaleID;
            saleDetail.ProductID = request.ProductID;

            await _saleDetailRepository.UpdateAsync(saleDetail);
            return Unit.Value;
        }
    }
}
