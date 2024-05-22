using BookSiteProject.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Dtos
{
    public class AuthorDtoValidator :AbstractValidator<AuthorDto>
    {
        public AuthorDtoValidator(IAuthorRepository authorRepository) {

            RuleFor(x => x.Firstname)
               .NotEmpty()
               .MinimumLength(2)
               .WithMessage("Name must be at least 2 characters long")
               .MaximumLength(20)
               .WithMessage("Title cannot be longer than 20 characters long")
                .Custom((value, context) =>
                {
                    var authorFromDb = authorRepository.GetAuthorByFirstname(value).Result;
                    if (authorFromDb != null)
                    {
                        Console.Out.WriteLine("Author exists");
                        context.AddFailure("Author this name already exist!");
                    }
                });


            RuleFor(x => x.Surname)
               .MinimumLength(2)
               .WithMessage("Name must be at least 2 characters long")
               .MaximumLength(20)
               .WithMessage("Title cannot be longer than 20 characters long");
              


        }
    }
}
