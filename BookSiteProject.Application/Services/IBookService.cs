using BookSiteProject.Application.Dtos;
using BookSiteProject.Domain.Entities;

namespace BookSiteProject.Application.Services
{
    public interface IBookService
    {
        Task Create(BookDto book);
        Task<IEnumerable<BookDto>> GetAll();
    }
}