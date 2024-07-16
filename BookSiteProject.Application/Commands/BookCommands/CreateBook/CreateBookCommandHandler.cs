using AutoMapper;
using BookSiteProject.Application.ApplicationUser;
using BookSiteProject.Application.Commands.BookCommands.CreateBook;
using BookSiteProject.Application.Dtos;
using BookSiteProject.Domain.Entities;
using BookSiteProject.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _webHostEnvironment; 

        public CreateBookCommandHandler(
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            IMapper mapper,
            ICategoryRepository categoryRepository,
            IUserContext userContext,
            IWebHostEnvironment webHostEnvironment) 
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _userContext = userContext;
            _webHostEnvironment = webHostEnvironment; 
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

            if (request.BookImage != null)
            {
                var fileName = Path.GetFileName(request.BookImage.FileName);
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.BookImage.CopyToAsync(stream);
                }

                book.ImagePath = "/images/" + fileName;
            }

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
