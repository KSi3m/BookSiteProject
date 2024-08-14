using BookSiteProject.Application.Dtos;
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

namespace BookSiteProject.Application.Commands.CategoryCommands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {

        public CreateCategoryCommandValidator(ICategoryRepository categoryRepository)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2)
                .WithMessage("Name must be at least 2 characters long")
                .MaximumLength(20)
                .WithMessage("Title cannot be longer than 20 characters long")

            .Custom((value, context) =>
            {
                var categoryFromDb = categoryRepository.GetCategoryByName(value).Result;

                if (categoryFromDb != null)
                {
                    context.AddFailure("Name", "Category with this name already exist!");
                }
            });
        }

    }
}
