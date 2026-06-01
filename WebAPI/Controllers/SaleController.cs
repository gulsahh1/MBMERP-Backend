using Application.Features.Commands.Payments;
using Application.Features.Commands.Sales;
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
    public class SaleController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SaleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetSale()
        {
            var result = await _mediator.Send(new GetSaleQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale(CreateSaleCommand saleCommand)
        {
            var id = await _mediator.Send(saleCommand);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSalent(UpdateSaleCommand updateSale)
        {
            await _mediator.Send(updateSale);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            await _mediator.Send(new DeleteSaleCommand { SaleID = id });
            return Ok();
        }
    }
}
