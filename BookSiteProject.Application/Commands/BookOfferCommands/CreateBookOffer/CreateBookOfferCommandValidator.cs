using BookSiteProject.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Commands.BookOfferCommands.CreateBookOffer
{
    public class CreateBookOfferCommandValidator: AbstractValidator<CreateBookOfferCommand>
    {


        public CreateBookOfferCommandValidator(IBookOfferRepository bookOfferRepository) {

            RuleFor(x => x.BookEncodedName).NotEmpty().NotNull();
            RuleFor(x=>x.Price).NotEmpty().NotNull();
            //RuleFor(x=>x.Status).NotEmpty().NotNull();
            //RuleFor(x=>x.Type).NotEmpty();
           // RuleFor(x=>x.DateOfCreation).NotEmpty().NotNull();
        }
    }
}
