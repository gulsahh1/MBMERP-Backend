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
    public class UpdatePaymentCommand : IRequest<Unit>
    {
        public int PaymentID { get; set; }
        public int CustomerID { get; set; }
        public decimal Amount { get; set; } 
        public DateTime Date { get; set; }
        public PaymentType PaymentType { get; set; }
    }

    public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, Unit>
    {
        private readonly IPaymentRepository _paymentRepository;

        public UpdatePaymentCommandHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Unit> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByIdAsync(request.PaymentID);

            if (payment == null)
                throw new Exception("Silenecek kayıt bulunamadı");

            payment.Amount = request.Amount;
            payment.PaymentType = request.PaymentType;
            payment.CustomerID = request.CustomerID;

            await _paymentRepository.UpdateAsync(payment);
            return Unit.Value;
        }
    }
}
