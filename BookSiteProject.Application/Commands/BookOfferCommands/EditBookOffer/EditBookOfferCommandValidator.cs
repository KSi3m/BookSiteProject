using BookSiteProject.Application.Commands.BookCommands.EditBook;
using BookSiteProject.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Commands.BookOfferCommands.EditBookOffer
{
    public class EditBookOfferCommandValidator: AbstractValidator<EditBookOfferCommand>
    {
        public EditBookOfferCommandValidator(IBookOfferRepository bookOfferRepository)
        {
            RuleFor(x => x.Price).NotEmpty().NotNull();

        }
    }
}
