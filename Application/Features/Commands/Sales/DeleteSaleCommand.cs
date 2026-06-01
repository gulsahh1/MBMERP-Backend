using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using MediatR;

namespace Application.Features.Commands.Sales
{
    public class DeleteSaleCommand :IRequest<Unit>
    {
        public int SaleID { get; set; }
    }

    public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand, Unit>
    {
        private readonly ISaleRepository _saleRepository;

        public DeleteSaleCommandHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<Unit> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleID);

            if (sale == null)
                throw new Exception("Satış Kaydı Bulunamadı");

            sale.IsActive = false;

            await _saleRepository.UpdateAsync(sale);

            return Unit.Value;
        }
    }
}
