using AutoMapper;
using BookSiteProject.Application.ApplicationUser;
using BookSiteProject.Application.Commands.BookCommands.CreateBook;
using BookSiteProject.Application.Mappings;
using BookSiteProject.Application.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
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
            //services.AddScoped<IBookService,BookService>();
            services.AddScoped<IUserContext, UserContext>();

            services.AddMediatR(typeof(CreateBookCommand));
            //services.AddScoped<IAuthorService, AuthorService>();
            //services.AddScoped<ICategoryService, CategoryService>();

            services.AddAutoMapper(typeof(BookMappingProfile));
            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                var scope = provider.CreateScope();
                var userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();
                cfg.AddProfile(new BookMappingProfile(userContext));
            }).CreateMapper());

            services.AddValidatorsFromAssemblyContaining<CreateBookCommandValidator>()
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
