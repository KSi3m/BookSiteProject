using BookSiteProject.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Commands.CategoryCommands.EditCategory
{
    public class EditCategoryCommand: IRequest
    {
        public string OldName { get; set; }
        public string NewName { get; set; }
    }
}
