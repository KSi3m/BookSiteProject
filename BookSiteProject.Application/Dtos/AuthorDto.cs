using BookSiteProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Dtos
{
    public class AuthorDto
    {
    
        public string Firstname { get; set; } = default!;
        public string? Surname { get; set; }
     
        public IEnumerable<int>? Books { get; set; }
    }
}
