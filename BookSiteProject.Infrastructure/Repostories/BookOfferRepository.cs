using BookSiteProject.Domain.Entities;
using BookSiteProject.Domain.Interfaces;
using BookSiteProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
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

        public async Task Commit()
        {
            await _dbcontext.SaveChangesAsync();
        }

        public async Task Create(BookOffer bookOffer)
        {
            _dbcontext.Add(bookOffer);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookOffer>> GetAllBookOffersByEncodedName(string bookEncodedName)
        {
            return await _dbcontext.BookOffers
                .Where(x => x.Book.EncodedName == bookEncodedName)
                .Include(x => x.CreatedBy)
                .ToListAsync();
        }

        public async Task<BookOffer> GetBookOfferById(int offerId)
        {
            return await _dbcontext.BookOffers.Where(x => x.Id == offerId).FirstOrDefaultAsync();
        }

        public async Task Remove(BookOffer bookOffer)
        {
            _dbcontext.BookOffers.Remove(bookOffer);
            await _dbcontext.SaveChangesAsync();
        }

    }
}
