using AutoMapper;
using BookSiteProject.Application.Dtos;
using BookSiteProject.Domain.Entities;
using BookSiteProject.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository,
                           IMapper mapper, ICategoryRepository categoryRepository)
        {
            this._bookRepository = bookRepository;
            this._authorRepository = authorRepository;
            this._mapper = mapper;
            this._categoryRepository = categoryRepository;
        }
        public async Task Create(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            book.Category = await _categoryRepository.GetCategoryById(bookDto.CategoryId);
            book.Authors = (List<Author>)await _authorRepository.GetAuthorsById(bookDto.AuthorsIds);
            book.EncodeName();
          
            await _bookRepository.Create(book);
        }

        public async Task<IEnumerable<BookDto>> GetAll()
        {
            var books = await _bookRepository.GetAll();
            var dtos = _mapper.Map<IEnumerable<BookDto>>(books);
            return dtos;
            //return await _bookRepository.Create(book);
        }
    }
}
