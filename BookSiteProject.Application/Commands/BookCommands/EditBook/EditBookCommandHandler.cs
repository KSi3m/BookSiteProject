using AutoMapper;
using BookSiteProject.Application.ApplicationUser;
using BookSiteProject.Application.Commands.BookCommands.CreateBook;
using BookSiteProject.Application.Dtos;
using BookSiteProject.Application.Queries.GetBookByEncodedName;
using BookSiteProject.Domain.Entities;
using BookSiteProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Commands.BookCommands.EditBook
{
    public class EditBookCommandHandler : IRequestHandler<EditBookCommand>
    {

        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserContext _userContext;
        private readonly IAuthorRepository _authorRepository;

        public EditBookCommandHandler(IBookRepository bookRepository, IMapper mapper, IAuthorRepository authorRepository,
                                 ICategoryRepository categoryRepository, IUserContext userContext)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(EditBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetBookByEncodedName(request.EncodedName);

            var user = _userContext.GetCurrentUser();
            if (user == null && (book.CreatedById != user.Id || user.IsInRole("Moderator")))
            {
                return Unit.Value;
            }

            book.Title = request.Title;
            book.Description = request.Description;
            book.YearOfPublication = request.YearOfPublication;
            book.Publisher = request.Publisher;
            book.Price = request.Price;
            book.ISBN = request.ISBN;
            book.typeOfBookOwnership = (TypeOfBookOwnership)request.typeOfBookOwnership;
            book.Category = await _categoryRepository.GetCategoryById(request.CategoryId);
            book.Authors = (List<Author>)await _authorRepository.GetAuthorsById(request.AuthorsIds);
           // book.EncodeName();

            await _bookRepository.Commit();
            return Unit.Value;
        }


        
        public async Task<BookDto> Handle(GetBookByEncodedNameQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetBookByEncodedName(request.EncodedName);
            var bookDto = _mapper.Map<BookDto>(book);
            return bookDto;
        }
    }
}
