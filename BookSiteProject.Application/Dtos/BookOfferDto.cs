using BookSiteProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static BookSiteProject.Domain.Entities.BookOffer;

namespace BookSiteProject.Application.Dtos
{
    public class BookOfferDto
    {
        public int Id { get; set; }
        public OfferType Type { get; set; }
        public OfferStatus Status { get; set; }
        public float? Price { get; set; } 
        public DateTime DateOfCreation { get; set; }

        public string? CreatedBy { get; set; }

    }
}
