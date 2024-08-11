using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Commands.CategoryCommands.DeleteCategory
{
    public class DeleteCategoryCommand: IRequest
    {
        public string CategoryName { get; set; }
    }
}
