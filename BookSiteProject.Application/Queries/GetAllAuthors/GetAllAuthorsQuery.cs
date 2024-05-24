using BookSiteProject.Application.Dtos;
using BookSiteProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Queries.GetAllAuthors
{
    public class GetAllAuthorsQuery: IRequest<IEnumerable<Author>>
    {
    }
}
