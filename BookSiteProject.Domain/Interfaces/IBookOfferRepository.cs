using BookSiteProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Domain.Interfaces
{
    public interface IBookOfferRepository
    {
        Task Create(BookOffer bookOffer);
        Task<IEnumerable<BookOffer>> GetAllBookOffersByEncodedName(string bookEncodedName);
    }
}
