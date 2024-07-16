using AutoMapper;
using BookSiteProject.Application.ApplicationUser;
using BookSiteProject.Application.Commands.BookCommands.CreateBook;
using BookSiteProject.Application.Dtos;
using BookSiteProject.Domain.Entities;
using BookSiteProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Commands.BookCommands.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand>
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
        public CreateBookCommandHandler(IBookRepository bookRepository, IAuthorRepository authorRepository,
                                IMapper mapper, ICategoryRepository categoryRepository,
                                IUserContext userContext)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            bool isEditable = currentUser != null && (currentUser.IsInRole("Owner") || currentUser.IsInRole("Admin") || currentUser.IsInRole("Moderator"));
            if (!isEditable)
            {
                 return Unit.Value;
            }
            var book = _mapper.Map<Book>(request);
            book.Category = await _categoryRepository.GetCategoryById(request.CategoryId);
            book.Authors = (List<Author>)await _authorRepository.GetAuthorsById(request.AuthorsIds);
            do
            {
                book.EncodeName();
            } while (await _bookRepository.CheckIfBooksEncodedNameAlreadyInDb(book.EncodedName));


            book.CreatedById = currentUser.Id;

            await _bookRepository.Create(book);
            return Unit.Value;
        }
    }
}
