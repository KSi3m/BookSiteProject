using AutoMapper;
using BookSiteProject.Application.Dtos;
using BookSiteProject.Application.Queries.GetAllAuthors;
using BookSiteProject.Domain.Entities;
using BookSiteProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Queries.GetAllAuthorsDto
{
    public class GetAllCategoriesDtoQueryHandler : IRequestHandler<GetAllAuthorsDtoQuery, IEnumerable<AuthorDto>>
    {
      
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetAllCategoriesDtoQueryHandler( 
            IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IEnumerable<AuthorDto>> Handle(GetAllAuthorsDtoQuery request, CancellationToken cancellationToken)
        {
            var authors = await _mediator.Send(new GetAllAuthorsQuery());
            var dtos = _mapper.Map<IEnumerable<AuthorDto>>(authors);
            return dtos;
        }
    }
}
