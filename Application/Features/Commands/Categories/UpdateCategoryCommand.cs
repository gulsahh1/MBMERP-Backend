using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using MediatR;

namespace Application.Features.Commands.Categories
{
    public class UpdateCategoryCommand : IRequest
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }

    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryID);

            if (category == null)
                throw new Exception("Kategori Bulunamadı");

            category.CategoryName = request.CategoryName;
            await _categoryRepository.UpdateAsync(category);

            return Unit.Value;
        }
    }
}
