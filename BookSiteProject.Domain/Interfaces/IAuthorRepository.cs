using BookSiteProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Domain.Interfaces
{
    public interface IAuthorRepository
    {
        Task Create(Author author);
        Task<IEnumerable<Author>> GetAll();
        Task<Author?> GetAuthorByFirstname(string firstname);
        Task<IEnumerable<Author>> GetAuthorsById(IEnumerable<int> ids);
       // Task<List<Author>> GetAuthorsById(IEnumerable<int> authorsIds);
    }
}
