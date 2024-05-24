using AutoMapper;
using BookSiteProject.Application.Dtos;
using BookSiteProject.Application.Queries.GetAllBooks;
using BookSiteProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Queries.GetBookByEncodedName
{
    public class GetBookByEncodedNameHandler : IRequestHandler<GetBookByEncodedNameQuery, BookDto>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public GetBookByEncodedNameHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper=mapper;
        }
        public async Task<BookDto> Handle(GetBookByEncodedNameQuery request, CancellationToken cancellationToken)
        {
            var book =  await _bookRepository.GetBookByEncodedName(request.EncodedName);
            var bookDto = _mapper.Map<BookDto>(book);
            return bookDto;
        }
    }
}
