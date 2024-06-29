using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Domain.Entities
{
    public enum TypeOfBookOwnership
    {
        BOUGHT,
        BORROWED
    }
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; } 
        public short? YearOfPublication { get; set; }
        public string? Publisher {  get; set; }
        public short? Price {  get; set; }

        public string? ISBN { get; set; }

        public string? CreatedById {  get; set; }
        public IdentityUser? CreatedBy {  get; set; }

        public TypeOfBookOwnership typeOfBookOwnership { get; set; }

        public List<Author> Authors { get; set; } = [];
        public Category? Category { get; set; }

        public string EncodedName { get; private set; } = default!;

        public void EncodeName()
        {
            /*var firstLetters = Authors.Select(author =>
            {
                string firstLetterOfFirstname = author.Firstname.Substring(0, 1);
                string firstLetterOfSurname = author.Surname != null ? author.Surname.Substring(0, 1) : string.Empty;
                return firstLetterOfFirstname+firstLetterOfSurname;
            });


            EncodedName = (Title + "-" + string.Join("", firstLetters)).Replace(" ", "-").ToLower();*/
            if(EncodedName == null)
            {
                EncodedName = Guid.NewGuid().ToString("N").Substring(0, 8);
            }
            
        }

    }
}
