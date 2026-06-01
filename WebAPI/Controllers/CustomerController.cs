using Application.Features.Commands.Customers;
using Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetCustomerQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerCommand customerCommand)
        {
            var id = await _mediator.Send(customerCommand);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerCommand updateCustomer)
        {
            await _mediator.Send(updateCustomer);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _mediator.Send(new DeleteCustomerCommand { CustomerID = id });
            return Ok();
        }
    }
}
