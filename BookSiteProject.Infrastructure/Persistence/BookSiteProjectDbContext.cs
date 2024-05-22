﻿using BookSiteProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Infrastructure.Persistence
{
    public class BookSiteProjectDbContext: DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }

        public BookSiteProjectDbContext(DbContextOptions<BookSiteProjectDbContext> options) :base(options) { 
        
        }

       
    }
}