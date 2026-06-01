using Application.Features.Commands.Payments;
using Application.Features.Commands.SaleDetails;
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
    public class SaleDetailController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SaleDetailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetSaleDetail()
        {
            var result = await _mediator.Send(new GetSaleDetailQuery());
            return Ok(result);
        }

        [HttpGet("sale/{saleID}")]
        public async Task<IActionResult> GetBySaleID(int saleID)
        {
            var result = await _mediator.Send(new GetSaleDetailBySaleIdQuery
            {
                SaleID = saleID
            });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSaleDetail(CreateSaleDetailCommand saleDetailCommand)
        {
            var id = await _mediator.Send(saleDetailCommand);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSaleDetail( int id,[FromBody] UpdateSaleDetailCommand updateSaleDetail)
        {
            if (id != updateSaleDetail.SaleDetailID)
                return BadRequest();

            await _mediator.Send(updateSaleDetail);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSaleDetail(int id)
        {
            await _mediator.Send(new DeleteSaleDetailCommand { SaleDetailID = id });
            return Ok();
        }
    }
}
