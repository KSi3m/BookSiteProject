using AutoMapper;
using BookSiteProject.Application.Dtos;
using BookSiteProject.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Queries.BookOfferQueries.GetBookOfferById
{
    public class GetBookOfferByIdHandler : IRequestHandler<GetBookOfferByIdQuery, BookOfferDto>
    {
        private readonly IBookOfferRepository _bookOfferRepository;
        private readonly IMapper _mapper;

        public GetBookOfferByIdHandler(IBookOfferRepository bookOfferRepository, IMapper mapper)
        {
            _bookOfferRepository = bookOfferRepository;
            _mapper = mapper;
        }

        public async Task<BookOfferDto> Handle(GetBookOfferByIdQuery request, CancellationToken cancellationToken)
        {
            var bookOffer =  await _bookOfferRepository.GetBookOfferById(request.OfferId);
            var dto = _mapper.Map<BookOfferDto>(bookOffer);
            return dto;
        }
    }
}
