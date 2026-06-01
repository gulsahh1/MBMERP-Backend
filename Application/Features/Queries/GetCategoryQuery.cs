using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using MediatR;

namespace Application.Features.Queries
{
    public class GetCategoryQuery :IRequest<List<Category>>
    {
    }

    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, List<Category>>
    {
        private readonly ICategoryRepository _categorRepository;

        public GetCategoryQueryHandler(ICategoryRepository categorRepository)
        {
            _categorRepository = categorRepository;
        }

        public async Task<List<Category>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _categorRepository.GetAllAsync();
            return category.Where(x => x.isActive).ToList();

        }
    }
}
