using BookSiteProject.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Dtos
{
    public class BookDto
    {
      
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
      
        public short? YearOfPublication { get; set; }
        public string? Publisher { get; set; }
        //public short? Price { get; set; }

        public string? ISBN { get; set; }

       // public int typeOfBookOwnership { get; set; }

        public IEnumerable<int>? AuthorsIds { get; set; }
       
        public int? CategoryId { get; set; }

        public string? EncodedName { get;  set; }

        public bool IsEditable {  get; set; }
        public string? ImagePath { get; set; }
    }
}
