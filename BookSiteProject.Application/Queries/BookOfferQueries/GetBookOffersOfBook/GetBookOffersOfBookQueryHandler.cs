using AutoMapper;
using BookSiteProject.Application.Dtos;
using BookSiteProject.Application.Queries.GetAllBooks;
using BookSiteProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Queries.BookOfferQueries.GetBookOffersOfBook
{
    


    public class GetBookOffersOfBookQueryHandler : IRequestHandler<GetBookOffersOfBookQuery, IEnumerable<BookOfferDto>>
    {

        private readonly IBookOfferRepository _bookOfferRepository;
        private readonly IMapper _mapper;

        public GetBookOffersOfBookQueryHandler(IBookOfferRepository bookOfferRepository,
            IMapper mapper)
        {
            _bookOfferRepository = bookOfferRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookOfferDto>> Handle(GetBookOffersOfBookQuery request, CancellationToken cancellationToken)
        {
            var bookOffers = await _bookOfferRepository
                .GetAllBookOffersByEncodedName(request.EncodedName);
            var dtos = _mapper.Map<IEnumerable<BookOfferDto>>(bookOffers);
            return dtos;
        }
    }
}
