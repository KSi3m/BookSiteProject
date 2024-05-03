using BookSiteProject.Domain.Entities;
using BookSiteProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Infrastructure.Seeders
{
    public class BookSiteSeeder
    {
        private readonly BookSiteProjectDbContext _dbContext;
        public BookSiteSeeder(BookSiteProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task Seed()
        {
            if(await _dbContext.Database.CanConnectAsync())
            {
                var hasElements = _dbContext.Authors.Any()
                           || _dbContext.Books.Any()
                           || _dbContext.Categories.Any();
              
                if(!hasElements)
                {
                    var author1 = new Author()
                    {
                        Firstname = "Adam",
                        Surname = "Milosz",
                    };

                    var author2 = new Author()
                    {
                        Firstname = "Marian",
                        Surname = "Paździoch",
                    };

                    var category1 = new Category()
                    {
                        Name = "Sci-Fi",
                    };
                    var category2 = new Category()
                    {
                        Name = "Fantasy",
                    };

                    var book1 = new Book()
                    {
                        Title = "Test1",
                        YearOfPublication = 2004,
                        typeOfBookOwnership = TypeOfBookOwnership.BORROWED,
                        Category = category1,
                        Authors = new List<Author> { author1 }
                        

                    };
                    book1.EncodeName();
                    _dbContext.Authors.Add(author1);
                    _dbContext.Authors.Add(author2);
                    _dbContext.Categories.Add(category1);
                    _dbContext.Categories.Add(category2);
                    _dbContext.Books.Add(book1);
                    await _dbContext.SaveChangesAsync();


                }
            }
        }
    }
}
