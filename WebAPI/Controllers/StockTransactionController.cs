using Application.Features.Commands.Payments;
using Application.Features.Commands.StockTransactions;
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
    public class StockTransactionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StockTransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetStockTransaction()
        {
            var result = await _mediator.Send(new GetStockTransactionQuery());
            return Ok(result);
        }

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetByProductId(int productId)
        {
            var result = await _mediator.Send(
                new GetStockTransactionsByProductIdQuery
                {
                    ProductID = productId
                });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStockTransaction(CreateStockTransactionCommand createStockTransaction)
        {
            var id = await _mediator.Send(createStockTransaction);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStockTransaction(UpdateStockTransactionCommand updateStockTransaction)
        {
            await _mediator.Send(updateStockTransaction);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockTransaction(int id)
        {
            await _mediator.Send(new DeleteStockTransactionCommand { StockTransactionID = id });
            return Ok();
        }
    }
}
