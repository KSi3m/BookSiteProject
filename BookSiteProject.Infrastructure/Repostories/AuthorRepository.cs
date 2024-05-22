using BookSiteProject.Domain.Entities;
using BookSiteProject.Domain.Interfaces;
using BookSiteProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Infrastructure.Repostories
{
    public class AuthorRepository: IAuthorRepository
    {
        private readonly BookSiteProjectDbContext _dbcontext;

        public AuthorRepository(BookSiteProjectDbContext dbcontext)
        {
            this._dbcontext = dbcontext;
        }
        public async Task Create(Author author)
        {
            _dbcontext.Add(author);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            return await _dbcontext.Authors.ToListAsync();
        }

        public async Task<Author?> GetAuthorByFirstname(string firstname)
        {
            return await _dbcontext.Authors.FirstOrDefaultAsync(x => x.Firstname == firstname);
        }

        public async Task<IEnumerable<Author>> GetAuthorsById(IEnumerable<int> ids)
        {
            return await _dbcontext.Authors.Where(x => ids.Contains(x.Id)).ToListAsync();
        }
    }
}
