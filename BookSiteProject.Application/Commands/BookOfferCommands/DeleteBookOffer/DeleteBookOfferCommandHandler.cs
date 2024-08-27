using AutoMapper;
using BookSiteProject.Application.ApplicationUser;
using BookSiteProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Commands.BookOfferCommands.DeleteBookOffer
{
    public class DeleteBookOfferCommandHandler : IRequestHandler<DeleteBookOfferCommand>
    {
        private readonly IBookOfferRepository _bookOfferRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public DeleteBookOfferCommandHandler(IBookOfferRepository bookOfferRepository, IMapper mapper, IUserContext userContext)
        {
            _bookOfferRepository = bookOfferRepository;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(DeleteBookOfferCommand request, CancellationToken cancellationToken)
        {
            var bookOffer = await _bookOfferRepository.GetBookOfferById(request.OfferId);

            if (bookOffer == null) return Unit.Value;

            var user = _userContext.GetCurrentUser();
            bool isDeletable = user != null && (bookOffer.CreatedById == user.Id || user.IsInRole("Moderator") || user.IsInRole("Admin"));
           
            if(!isDeletable) return Unit.Value;
            await _bookOfferRepository.Remove(bookOffer);

            return Unit.Value;

        }
    }
}
