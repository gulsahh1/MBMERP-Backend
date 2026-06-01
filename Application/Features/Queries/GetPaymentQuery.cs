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
    public class GetPaymentQuery: IRequest<List<Payment>>
    {
    }
    public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, List<Payment>>
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetPaymentQueryHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<List<Payment>> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetAllAsync();
            return payment.Where(x => x.IsActive).ToList();

        }
    }
}
