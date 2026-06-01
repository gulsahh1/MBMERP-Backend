using Application.Features.Commands.OrderDetails;
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
    public class OrderDetailController : ControllerBase
    {
        private readonly IMediator _mediator;
    
            public OrderDetailController(IMediator mediator)
            {
                _mediator = mediator;
            }

        [HttpGet]
        public async Task<IActionResult> GetOrderDetails()
        {
            var orderDetails = await _mediator.Send(new GetOrderDetailQuery());
            return Ok(orderDetails);
        }

        [HttpGet("order/{orderID}")]
        public async Task<IActionResult> GetByOrderID(int orderID)
        {
            var result = await _mediator.Send(new GetOrderDetailByOrderIdQuery
            {
                OrderID = orderID
            });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderDetail(CreateOrderDetailCommand orderDetailCommand)
        {
            var id = await _mediator.Send(orderDetailCommand);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderDetail(UpdateOrderDetailCommand updateOrderDetail)
        {
            await _mediator.Send(updateOrderDetail);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            await _mediator.Send(new DeleteOrderDetailCommand { OrderDetailID = id });
            return NoContent();
        }
    }
}
