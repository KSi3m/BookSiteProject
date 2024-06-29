using BookSiteProject.Domain.Entities;
using BookSiteProject.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                           || _dbContext.Categories.Any()
                           || _dbContext.Roles.Any()
                           || _dbContext.Users.Any()
                           || _dbContext.UserRoles.Any();

                if (!hasElements)
                {
                    //dodac admina i ownera do aspNetRoles
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
            
                    foreach(var x in new string[] { "Admin", "Moderator" })
                    {
                        var roleId = Guid.NewGuid().ToString();
                        _dbContext.Roles.Add(new IdentityRole
                        {
                            Id = roleId,
                            Name = x,
                            NormalizedName = x.ToUpper(),
                            ConcurrencyStamp = Guid.NewGuid().ToString(),
                        });

                        var userId = Guid.NewGuid().ToString();
                        var user = new IdentityUser
                        {
                            Id = userId,
                            UserName = x + "@booksite.com",
                            Email = x+"@booksite.com",
                            EmailConfirmed = true,
                            NormalizedEmail = x.ToUpper()+"@BOOKSITE.COM",
                            NormalizedUserName = x.ToUpper() + "@BOOKSITE.COM"
                        };

                        var passwordHasher = new PasswordHasher<IdentityUser>();
                        user.PasswordHash = passwordHasher.HashPassword(user, "zaq1@WSX");

                        _dbContext.Users.Add(user);

                        _dbContext.UserRoles.Add(new IdentityUserRole<string>
                        {
                            UserId = userId,
                            RoleId = roleId
                        });
                    }
                    await _dbContext.SaveChangesAsync();


                }
            }
        }
    }
}
