using BookSiteProject.Application.Dtos;
using BookSiteProject.Application.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using BookSiteProject.Application.Commands.CreateAuthor;
using MediatR;
using BookSiteProject.Application.Queries.GetAllAuthorsDto;

namespace BookSiteProject.MVC.Controllers.MVC
{
    public class AuthorController : Controller
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var authors = await _mediator.Send(new GetAllAuthorsDtoQuery());
            return View(authors);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorCommand command)
        {
            if (!ModelState.IsValid)
            {
                await Console.Out.WriteLineAsync("TtT");
                return View();
            }
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

    }
}
