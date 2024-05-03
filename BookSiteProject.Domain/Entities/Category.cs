using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Domain.Entities
{
    public class Category
    {
        public required int Id { get; set; }
        public string Name { get; set; } = default!;
        public List<Book> Books { get; set; } = [];
    }
}
