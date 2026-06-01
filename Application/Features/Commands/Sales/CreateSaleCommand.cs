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
    public class CreateSaleCommand :IRequest<int>
    {
        public int CustomerID { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
    }

    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, int>
    {
        private readonly ISaleRepository _saleRepository;

        public CreateSaleCommandHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<int> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = new Sale
            {
               CustomerID = request.CustomerID,
               Date = request.Date,
               TotalAmount = request.TotalAmount,
            };

            await _saleRepository.AddAsync(sale);
            return sale.SaleID;
        }
    }
}
 