using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Domain.Entities
{
    public class Author
    {
        public  int Id { get; set; }
        public string Firstname { get; set; } = default!;
        public string? Surname { get; set; }
        public List<Book> Books { get; set; } = [];
        public bool Active { get; set; } = true;
    }
}
