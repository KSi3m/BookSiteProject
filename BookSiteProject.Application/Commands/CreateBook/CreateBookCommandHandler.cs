using AutoMapper;
using BookSiteProject.Application.Dtos;
using BookSiteProject.Domain.Entities;
using BookSiteProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Commands.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand>
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        public CreateBookCommandHandler(IBookRepository bookRepository, IAuthorRepository authorRepository,
                                IMapper mapper, ICategoryRepository categoryRepository)
        {
            this._bookRepository = bookRepository;
            this._authorRepository = authorRepository;
            this._mapper = mapper;
            this._categoryRepository = categoryRepository;
        }
        public async Task<Unit> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<Book>(request);
            book.Category = await _categoryRepository.GetCategoryById(request.CategoryId);
            book.Authors = (List<Author>)await _authorRepository.GetAuthorsById(request.AuthorsIds);
            book.EncodeName();

            await _bookRepository.Create(book);

            return Unit.Value;
        }
    }
}
