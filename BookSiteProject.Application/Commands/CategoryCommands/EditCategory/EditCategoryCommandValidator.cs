using BookSiteProject.Domain.Interfaces;
using System;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Commands.CategoryCommands.EditCategory
{
    public class EditCategoryCommandValidator: AbstractValidator<EditCategoryCommand>
    {
        public EditCategoryCommandValidator(ICategoryRepository categoryRepository)
        {
            RuleFor(x => x.NewName)
                .NotEmpty()
                .MinimumLength(2)
                .WithMessage("Name must be at least 2 characters long")
                .MaximumLength(20)
                .WithMessage("Title cannot be longer than 20 characters long")

            .Custom((value, context) =>
            {
                //Console.WriteLineAsync(value);
                var categoryFromDb = categoryRepository.GetCategoryByName(value).Result;

                if (categoryFromDb != null)
                {
                   context.AddFailure("Name", "Category with this name already exist!");
                }
            });
        }

     
    }

}
