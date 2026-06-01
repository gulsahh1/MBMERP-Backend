using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Commands.Categories;
using Core.Entities;
using Core.Interfaces;
using MediatR;

namespace Application.Features.Commands.Sales
{
    public class UpdateSaleCommand :IRequest<Unit>
    {
        public int SaleID { get; set; }
        public int CustomerID { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, Unit>
    {
        private readonly ISaleRepository _saleRepository;

        public UpdateSaleCommandHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<Unit> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleID);

            if (sale == null)
                throw new Exception("Satış Kaydı Bulunamadı");

            sale.CustomerID = request.CustomerID;
            sale.Date = request.Date;
            sale.TotalAmount = request.TotalAmount;

            await _saleRepository.UpdateAsync(sale);

            return Unit.Value;
        }
    }
}
