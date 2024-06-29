using BookSiteProject.Application.Commands.BookCommands.CreateBook;
using BookSiteProject.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Commands.BookCommands.EditBook
{
    public class EditBookCommandValidator : AbstractValidator<EditBookCommand>
    {
        public EditBookCommandValidator(IBookRepository bookRepository)
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MinimumLength(2)
                .WithMessage("Title must be at least 2 characters long")
                .MaximumLength(20)
                .WithMessage("Title cannot be longer than 20 characters long");

            RuleFor(x => x.YearOfPublication)
                .InclusiveBetween((short)1, (short)DateTime.Now.Year)
                .WithMessage("Year must be between 1 and current Year");

            RuleFor(book => book.ISBN)
            .Matches(@"^\d{10}$|^\d{13}$")
            .WithMessage("ISBN must be 10 or 13 digits long")
            .Custom((value, context) =>
            {
                var encodedName = context.InstanceToValidate.EncodedName;

                var bookFromDb = bookRepository.GetByISBN(value).Result; 
                if (bookFromDb != null && encodedName != bookFromDb.EncodedName)
                {
                    context.AddFailure("ISBN unique number already exist!");
                }
            });

            RuleFor(x => x.AuthorsIds)
                .NotEmpty()
                .WithMessage("Authors field cannot be empty");

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage("Category field cannot be empty");
        }
    }
}
