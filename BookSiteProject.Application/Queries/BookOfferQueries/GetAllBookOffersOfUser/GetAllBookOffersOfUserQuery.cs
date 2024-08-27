using BookSiteProject.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Queries.BookOfferQueries.GetAllBookOffersOfUser
{
    public class GetAllBookOffersOfUserQuery: IRequest<IEnumerable<BookOfferDto>>
    {
        public string UserId { get; set; }
    }
}
