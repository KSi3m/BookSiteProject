using BookSiteProject.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Queries.GetBookByEncodedName
{
    public class GetBookByEncodedNameQuery: IRequest<BookDto>
    {
        public string EncodedName { get; set; }
        public GetBookByEncodedNameQuery(string encodedName)
        {
            EncodedName = encodedName;
        }
    }
}
