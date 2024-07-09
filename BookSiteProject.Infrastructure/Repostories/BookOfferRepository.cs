using BookSiteProject.Domain.Entities;
using BookSiteProject.Domain.Interfaces;
using BookSiteProject.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Infrastructure.Repostories
{
    public class BookOfferRepository: IBookOfferRepository
    {
        private readonly BookSiteProjectDbContext _dbcontext;

        public BookOfferRepository(BookSiteProjectDbContext dbcontext)
        {
            this._dbcontext = dbcontext;
        }

        public async Task Create(BookOffer bookOffer)
        {
            _dbcontext.Add(bookOffer);
            await _dbcontext.SaveChangesAsync();
        }

    }
}
