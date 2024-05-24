using BookSiteProject.Application.Commands.CreateBook;
using BookSiteProject.Application.Dtos;
using BookSiteProject.Application.Queries.GetAllAuthors;
using BookSiteProject.Application.Queries.GetAllBooks;
using BookSiteProject.Application.Queries.GetAllCategories;
using BookSiteProject.Application.Queries.GetBookByEncodedName;
using BookSiteProject.Application.Services;
using BookSiteProject.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookSiteProject.MVC.Controllers
{
    public class BookController: Controller
    {
        
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator= mediator;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _mediator.Send(new GetAllBooksQuery());
            return View(books);
        }

            [HttpGet]
        public async Task<IActionResult> Create()
        {
            var authors = await _mediator.Send(new GetAllAuthorsQuery());
            var authorList = authors.Select(a => new SelectListItem 
            { 
                Value = a.Id.ToString(), 
                Text = a.Firstname + " " + a.Surname 
            }).ToList();
            ViewBag.Authors = authorList;

            var categories = await _mediator.Send(new GetAllCategoriesQuery());
            var categoryItems = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(), 
                Text = c.Name 
            }).ToList();

            ViewBag.Categories = categoryItems;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [Route("Book/{encodedName}/Details")]
        public async Task<IActionResult> Details(string encodedName)
        {
            var dto = await _mediator.Send(new GetBookByEncodedNameQuery(encodedName));
            return View(dto);
        }

    }
}
