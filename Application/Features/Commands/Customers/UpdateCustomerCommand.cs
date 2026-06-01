using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using Core.Interfaces;
using MediatR;

namespace Application.Features.Commands.Customers
{
    public class UpdateCustomerCommand : IRequest<Unit>
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public decimal Balance { get; set; } = 0;
        public CustomerType CustomerType { get; set; }
    }

    public class UpdateCustomerCommandHandler :IRequestHandler<UpdateCustomerCommand , Unit>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerID);

            if (customer == null)
                throw new Exception("Müşteri Bulunamadı");

            customer.CustomerName = request.CustomerName;
            customer.Phone = request.Phone;
            customer.Email = request.Email;
            customer.Address = request.Address;
            customer.Balance = request.Balance;
            customer.CustomerType = request.CustomerType;

            await _customerRepository.UpdateAsync(customer);
            return Unit.Value;
        }
    }
}
