using AutoMapper;
using BookSiteProject.Application.ApplicationUser;
using BookSiteProject.Domain.Entities;
using BookSiteProject.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Commands.BookCommands.DeleteBook
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

         public DeleteBookCommandHandler(IBookRepository bookRepository,
             IMapper mapper, IUserContext userContext, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _userContext = userContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetBookByEncodedName(request.EncodedName);

            if (book == null) return Unit.Value;

            var user = _userContext.GetCurrentUser();
            bool isEditable = user != null && (book.CreatedById == user.Id || user.IsInRole("Admin"));
            if (!isEditable)
            {
                return Unit.Value;
            }

            var imagePath = book.ImagePath;
            if (!string.IsNullOrEmpty(imagePath))
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath.TrimStart('/'));
                Console.WriteLine(filePath);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            await _bookRepository.Remove(book);

            return Unit.Value;
        }
    }
}
