using AutoMapper;
using BookSiteProject.Domain.Entities;
using BookSiteProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Queries.CategoryQueries.GetAllListedCategories
{
    public class GetAllListedCategoriesQueryHandler : IRequestHandler<GetAllListedCategoriesQuery, IEnumerable<Category>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetAllListedCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Category>> Handle(GetAllListedCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetAllListed();
        }
    }
}
