using AutoMapper;
using BookSiteProject.Application.ApplicationUser;
using BookSiteProject.Domain.Entities;
using BookSiteProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Commands.BookOfferCommands.CreateBookOffer
{
    public class CreateBookOfferCommandHandler : IRequestHandler<CreateBookOfferCommand>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookOfferRepository _bookOfferRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public CreateBookOfferCommandHandler(IBookRepository bookRepository, 
            IMapper mapper, IUserContext userContext, IBookOfferRepository bookOfferRepository)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _userContext = userContext;
            _bookOfferRepository = bookOfferRepository;
        }

        public async Task<Unit> Handle(CreateBookOfferCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetBookByEncodedName(request.BookEncodedName);

            var user = _userContext.GetCurrentUser();
            //if (user == null && (book.CreatedById != user.Id || user.IsInRole("Moderator")))
            //bool isEditable = user != null && book.CreatedById == user.Id;
            bool isEditable = user != null ;
            if (!isEditable)
            {
                return Unit.Value;
            }
            var bookOffer = new BookOffer()
            {
                Price = request.Price,
                CreatedById = user.Id,
                BookId = book.Id,
                Type = request.Type,
                Status = BookOffer.OfferStatus.Available
            };


            await _bookOfferRepository.Create(bookOffer);
            return Unit.Value;
        }
    }
}
