using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using MediatR;

namespace Application.Features.Commands.Payments
{
    public class DeletePaymentCommand :IRequest<Unit>
    {
        public int PaymentID { get; set; }
    }


    public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, Unit>
    {
        private readonly IPaymentRepository _paymentRepository;

        public DeletePaymentCommandHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Unit> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
           var payment = await _paymentRepository.GetByIdAsync(request.PaymentID);
            if (payment == null)
                throw new Exception("Silenecek kayıt bulunamadı");

            payment.IsActive = false;

            await _paymentRepository.UpdateAsync(payment);
            return Unit.Value;
        }
    }
}
