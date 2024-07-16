using BookSiteProject.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Commands.BookCommands.CreateBook
{
    public class EditBookCommand: BookDto,IRequest
    {
        public IFormFile? BookImage { get; set; }
    }
}
