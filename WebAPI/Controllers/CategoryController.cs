using Application.Features.Commands.Categories;
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
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _meditor;

        public CategoryController(IMediator meditor)
        {
            _meditor = meditor;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _meditor.Send(new GetCategoryQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommand categoryCommand)
        {
            var id = await _meditor.Send(categoryCommand);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryCommand updateCategory)
        {
            await _meditor.Send(updateCategory);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _meditor.Send(new DeleteCategoryCommand { CategoryID = id });
            return Ok();
        }
    }
}
