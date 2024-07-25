using AutoMapper;
using BookSiteProject.Application.ApplicationUser;
using BookSiteProject.Application.Commands.BookCommands.CreateBook;
using BookSiteProject.Application.Dtos;
using BookSiteProject.Application.Queries.GetBookByEncodedName;
using BookSiteProject.Domain.Entities;
using BookSiteProject.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;
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
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IUserContext _userContext;
        private readonly IWebHostEnvironment _webHostEnvironment; 

        public EditBookCommandHandler(
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            ICategoryRepository categoryRepository,
            IUserContext userContext,
            IWebHostEnvironment webHostEnvironment) 
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
            _userContext = userContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Unit> Handle(EditBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetBookByEncodedName(request.EncodedName);

            var user = _userContext.GetCurrentUser();
            bool isEditable = user != null && (book.CreatedById == user.Id || user.IsInRole("Moderator"));
            if (!isEditable)
            {
                return Unit.Value;
            }

            book.Title = request.Title;
            book.Description = request.Description;
            book.YearOfPublication = request.YearOfPublication;
            book.Publisher = request.Publisher;
            //book.Price = request.Price;
            book.ISBN = request.ISBN;
            //book.typeOfBookOwnership = (TypeOfBookOwnership)request.typeOfBookOwnership;
            book.Category = await _categoryRepository.GetCategoryById(request.CategoryId);
            book.Authors = (List<Author>)await _authorRepository.GetAuthorsById(request.AuthorsIds);
            // book.EncodeName();
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
            

            await _bookRepository.Commit();
            return Unit.Value;
        }


        
        /*public async Task<BookDto> Handle(GetBookByEncodedNameQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetBookByEncodedName(request.EncodedName);
            var bookDto = _mapper.Map<BookDto>(book);
            return bookDto;
        }*/
    }
}
