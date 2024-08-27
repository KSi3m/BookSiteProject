using AutoMapper;
using BookSiteProject.Application.Dtos;
using BookSiteProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Queries.BookOfferQueries.GetAllBookOffersOfUser
{
    public class GetAllBookOffersOfUserQueryHandler : IRequestHandler<GetAllBookOffersOfUserQuery, IEnumerable<BookOfferDto>>
    {
        private readonly IBookOfferRepository _bookOfferRepository;
        private readonly IMapper _mapper;

        public GetAllBookOffersOfUserQueryHandler(IBookOfferRepository bookOfferRepository, IMapper mapper)
        {
            _bookOfferRepository = bookOfferRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookOfferDto>> Handle(GetAllBookOffersOfUserQuery request, CancellationToken cancellationToken)
        {
            var bookOffers = await _bookOfferRepository.GetAllBookOffersOfUser(request.UserId);
            var dtos = _mapper.Map<IEnumerable<BookOfferDto>>(bookOffers);

            return dtos;
            
        }
    }
}
