using Application.Interfaces;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Customers
{
    public class CreateCustomerCommand :IRequest<int>
    {
        public string CustomerName { get; set; } 
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public decimal Balance { get; set; } = 0;
        public CustomerType CustomerType { get; set; }
    }

    public class CreateCustomerCommandHandler :IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly INotificationService _notificationService;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository,INotificationService notificationService)
        {
            _customerRepository = customerRepository;
            _notificationService = notificationService;
        }

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                CustomerName = request.CustomerName,
                Phone = request.Phone,
                Email = request.Email,
                Address = request.Address,
                Balance = request.Balance,
                CustomerType = request.CustomerType,

            };

            await _customerRepository.AddAsync(customer);

            await _notificationService.CreateAsync(
                                "Yeni Müşteri",
                                $"{customer.CustomerName} sisteme eklendi",
                              NotificationType.CustomerCreated
                              );

          

            return customer.CustomerID;
        }
    }
}
