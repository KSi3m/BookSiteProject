using BookSiteProject.Application.Services;
using BookSiteProject.Domain.Entities;
using BookSiteProject.Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Dtos
{
    public class CategoryDtoValidator : AbstractValidator<CategoryDto>
    {

        public CategoryDtoValidator(ICategoryRepository categoryRepository)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2)
                .WithMessage("Name must be at least 2 characters long")
                .MaximumLength(20)
                .WithMessage("Title cannot be longer than 20 characters long")
               
            .Custom((value, context) =>
            {
                var categoryFromDb = categoryRepository.GetCategoryByName(value);
                if (categoryFromDb != null)
                {
                    Console.Out.WriteLine("Book exists");
                    context.AddFailure("Name", "Category with this name already exist!");
                }
            });
        }
      
    }
}
