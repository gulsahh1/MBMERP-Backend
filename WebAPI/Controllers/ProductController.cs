using Application.Features.Commands.Categories;
using Application.Features.Commands.Products;
using Application.Features.Queries;
using Core.Interfaces;
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
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            var result = await _mediator.Send(new GetProductQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand productCommand)
        {
            var id = await _mediator.Send(productCommand);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand updateProduct)
        {
            await _mediator.Send(updateProduct);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _mediator.Send(new DeleteProductCommand { ProductID = id });
            return Ok();
        }
    }
}
