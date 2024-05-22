using BookSiteProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Dtos
{
    public class CategoryDto
    {
        public string Name { get; set; } = default!;
        public IEnumerable<int>? BooksIds { get; set; }
    }
}
