using BookSiteProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Queries.CategoryQueries.GetAllListedCategories
{
    public class GetAllListedCategoriesQuery: IRequest<IEnumerable<Category>>
    {
    }
}
