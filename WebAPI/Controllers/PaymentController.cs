using Application.Features.Commands.Payments;
using Application.Features.Commands.Products;
using Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetPayment()
        {
            var result = await _mediator.Send(new GetPaymentQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment(CreatePaymentCommand paymentCommand)
        {
            var id = await _mediator.Send(paymentCommand);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(UpdatePaymentCommand updatePayment)
        {
            await _mediator.Send(updatePayment);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            await _mediator.Send(new DeletePaymentCommand { PaymentID = id });
            return Ok();
        }
    }
}
