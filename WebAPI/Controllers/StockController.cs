using Application.Features.Commands.Payments;
using Application.Features.Commands.Stocks;
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
    public class StockController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetStock()
        {
            var result = await _mediator.Send(new GetStockQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock(CreateStockCommand stockCommand)
        {
            var id = await _mediator.Send(stockCommand);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock(UpdateStockCommand updateStock)
        {
            await _mediator.Send(updateStock);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            await _mediator.Send(new DeleteStockCommand { StockID = id });
            return Ok();
        }
    }
}
