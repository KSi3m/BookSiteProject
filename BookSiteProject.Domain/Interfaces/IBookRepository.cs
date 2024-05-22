using BookSiteProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task Create(Book book);
        Task<IEnumerable<Book>> GetAll();
        Task<Book?> GetByISBN(string ISBN);
    }
}
