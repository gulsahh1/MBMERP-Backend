using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using MediatR;

namespace Application.Features.Commands.Payments
{
    public class CreatePaymentCommand :IRequest<int>
    {
        public int CustomerID { get; set; }
        public decimal Amount { get; set; } 
        public DateTime Date { get; set; } = DateTime.Now;
        public PaymentType PaymentType { get; set; }
    }

    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, int>
    {
        private readonly IPaymentRepository _paymentRepository;

        public CreatePaymentCommandHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<int> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = new Payment
            {
                Amount = request.Amount,
                PaymentType = request.PaymentType,
                CustomerID = request.CustomerID,
            };

            await _paymentRepository.AddAsync(payment);
            return payment.PaymentID;
        }
    }
}
