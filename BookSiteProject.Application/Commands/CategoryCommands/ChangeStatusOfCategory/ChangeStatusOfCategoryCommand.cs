using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Commands.CategoryCommands.ChangeStatusOfCategory
{
    public class ChangeStatusOfCategoryCommand: IRequest
    {
        public string CategoryName { get; set;}
        public bool Status { get; set;}
    }
}
