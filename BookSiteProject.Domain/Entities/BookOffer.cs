using BookSiteProject.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Domain.Entities
{
    public class BookOffer
    {
        public enum OfferType { Sale=0, Rental=1 }
        public enum OfferStatus { Available, Unavailable }
        public int Id { get; set; }      
        public OfferType Type { get; set; }
        public OfferStatus Status { get; set; }
        public float? Price { get; set; }
        public string? CreatedById { get; set; }
        public ApplicationUser? CreatedBy { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.UtcNow;

        public int BookId { get; set; } = default!;
        public Book Book { get; set; } = default!;
    }
}
