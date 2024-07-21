using BookSiteProject.Application.Dtos;
using BookSiteProject.Domain.Entities;

namespace BookSiteProject.Application.Services
{
    public interface IAuthorService
    {
        Task Create(AuthorDto authorDto);
        Task<IEnumerable<Author>> GetAuthors();

        Task<IEnumerable<AuthorDto>> GetAllAuthorsDto();
      
    }
}