using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using MediatR;

namespace Application.Features.Commands.SaleDetails
{
    public class DeleteSaleDetailCommand :IRequest<Unit>
    {
        public int SaleDetailID { get; set; }
    }

    public class DeleteSaleDetailCommandHandler : IRequestHandler<DeleteSaleDetailCommand, Unit>
    {
        private readonly ISaleDetailRepository _saleDetailRepository;

        public DeleteSaleDetailCommandHandler(ISaleDetailRepository saleDetailRepository)
        {
            _saleDetailRepository = saleDetailRepository;
        }

        public async Task<Unit> Handle(DeleteSaleDetailCommand request, CancellationToken cancellationToken)
        {
            var saleDetail = await _saleDetailRepository.GetByIdAsync(request.SaleDetailID);

            if (saleDetail == null)
                throw new Exception("Kayıt Bulunamadı");

            saleDetail.IsActive = false;

            await _saleDetailRepository.UpdateAsync(saleDetail);
            return Unit.Value;
        }
    }
}
