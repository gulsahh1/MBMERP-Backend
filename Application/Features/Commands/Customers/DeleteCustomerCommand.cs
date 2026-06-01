using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using MediatR;

namespace Application.Features.Commands.Customers
{
    public class DeleteCustomerCommand : IRequest<Unit>
    {
        public int CustomerID { get; set; }
    }

    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Unit>
    {
        private readonly ICustomerRepository _customerRepository;

        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerID);

            if (customer == null)
                throw new Exception("Silinecek Müşteri Bulunamadı");

            customer.isActive = false;

            await _customerRepository.UpdateAsync(customer);
            return Unit.Value;
        }
    }
}
