using BookSiteProject.Application.Dtos;
using BookSiteProject.Application.Mappings;
using BookSiteProject.Application.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IBookService,BookService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddAutoMapper(typeof(BookMappingProfile));

            services.AddValidatorsFromAssemblyContaining<BookDtoValidator>()
                //.AddValidatorsFromAssemblyContaining<CategoryDtoValidator>()
                //.AddValidatorsFromAssemblyContaining<AuthorDtoValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

            /*services.AddValidatorsFromAssemblyContaining<CategoryDtoValidator>()
               .AddFluentValidationAutoValidation()
               .AddFluentValidationClientsideAdapters();*/
        }
    }
}
