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
    public class GetSaleDetailBySaleIdQuery :IRequest<List<SaleDetail>>
    {
        public int SaleID { get; set; }
    }

    public class GetSaleDetailBySaleIDQueryHandler : IRequestHandler<GetSaleDetailBySaleIdQuery, List<SaleDetail>>
    {
        private readonly ISaleDetailRepository _saleDetailRepository;

        public GetSaleDetailBySaleIDQueryHandler(ISaleDetailRepository saleDetailRepository)
        {
            _saleDetailRepository = saleDetailRepository;
        }

        public async Task<List<SaleDetail>> Handle(GetSaleDetailBySaleIdQuery request, CancellationToken cancellationToken)
        {
            return await _saleDetailRepository.GetBySaleIdAsync(request.SaleID);
        }
    }
}
