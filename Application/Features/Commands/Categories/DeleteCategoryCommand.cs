using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using MediatR;

namespace Application.Features.Commands.Categories
{
    public class DeleteCategoryCommand : IRequest
    {
        public int CategoryID { get; set; }
    }

    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryID);

            if (category == null)
                throw new Exception("Silinecek Kategori Bulunamadı!");

            category.isActive = false;

            await _categoryRepository.UpdateAsync(category);
            return Unit.Value;
        }
    }
}
