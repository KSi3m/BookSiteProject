using AutoMapper;
using BookSiteProject.Application.Dtos;
using BookSiteProject.Application.Queries.CategoryQueries.GetAllCategories;
using BookSiteProject.Domain.Entities;
using BookSiteProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Queries.CategoryQueries.GetAllCategoriesDto
{
    public class GetAllCategoriesDtoQueryHandler : IRequestHandler<GetAllCategoriesDtoQuery, IEnumerable<CategoryDto>>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetAllCategoriesDtoQueryHandler(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesDtoQuery request, CancellationToken cancellationToken)
        {
            var categories = await _mediator.Send(new GetAllCategoriesQuery());
            var dtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return dtos;
        }

    }
}
