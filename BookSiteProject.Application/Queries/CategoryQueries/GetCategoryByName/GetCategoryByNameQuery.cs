using BookSiteProject.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Queries.CategoryQueries.GetCategoryByName
{
    public class GetCategoryByNameQuery: IRequest<CategoryDto>
    {
        public string Name { get; set; }

        public GetCategoryByNameQuery(string name)
        {
            Name = name;
        }
    }
}
