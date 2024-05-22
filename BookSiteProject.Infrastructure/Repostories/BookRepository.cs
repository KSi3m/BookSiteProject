﻿using BookSiteProject.Domain.Entities;
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
    public class BookRepository : IBookRepository
    {
        private readonly BookSiteProjectDbContext _dbcontext;

        public BookRepository(BookSiteProjectDbContext dbcontext)
        {
            this._dbcontext = dbcontext;
        }



        public async Task Create(Book book)
        {
            _dbcontext.Add(book);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _dbcontext.Books
                .Include(b => b.Category)
                .Include(b => b.Authors)
                .ToListAsync();
        }

        public async Task<Book?> GetByISBN(string ISBN)
        {
            if (ISBN == null)
            {
                return null;
            }
            return await  _dbcontext.Books
                .FirstOrDefaultAsync(x => x.ISBN != null && x.ISBN.ToLower() == ISBN.ToLower());
        }



    }
}