using BookSiteProject.Application.ApplicationUser;
using BookSiteProject.Domain.Entities;
using BookSiteProject.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Commands.BookOfferCommands.EditBookOffer
{
    public class EditBookOfferCommandHandler : IRequestHandler<EditBookOfferCommand>
    {
        private readonly IBookOfferRepository _bookOfferRepository;
        private readonly IUserContext _userContext;

        public EditBookOfferCommandHandler(IBookOfferRepository bookOfferRepository, IUserContext userContext)
        {
            _bookOfferRepository = bookOfferRepository;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(EditBookOfferCommand request, CancellationToken cancellationToken)
        {
            var bookOffer = await _bookOfferRepository.GetBookOfferById(request.Id);

            var user = _userContext.GetCurrentUser();
            bool isEditable = user != null && (bookOffer.CreatedById == user.Id || user.IsInRole("Moderator") || user.IsInRole("Admin"));
            if (!isEditable)
            {
                return Unit.Value;
            }

            bookOffer.Status = request.Status;
            bookOffer.Price = request.Price;
            bookOffer.Type = request.Type;
            //bookOffer.LastModification = request.LastModification;


            await _bookOfferRepository.Commit();
            return Unit.Value;
        }
    }
}
