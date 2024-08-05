﻿using AutoMapper;
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
            bool isEditable = user != null && (book.CreatedById == user.Id || user.IsInRole("Moderator") || user.IsInRole("Admin"));
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
                //adding new file to /images
                var fileExtension = Path.GetExtension(request.BookImage.FileName);
                var fileName = Guid.NewGuid().ToString() + fileExtension;
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.BookImage.CopyToAsync(stream);
                }
                //deleting old file
                var imagePath = book.ImagePath;
                if (!string.IsNullOrEmpty(imagePath))
                {
                    var oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath.TrimStart('/'));
                    if (File.Exists(oldFilePath))
                    {
                        File.Delete(oldFilePath);
                    }
                }
                //setting new file for book
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
