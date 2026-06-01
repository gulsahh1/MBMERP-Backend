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
    public class GetCustomerQuery :IRequest<List<Customer>>
    {
    }

    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, List<Customer>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<Customer>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetAllAsync();
            return customer.Where(x=>x.isActive).ToList();
        } 
    }
}
