using BookSiteProject.Application.Dtos;
using BookSiteProject.Application.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using BookSiteProject.Domain.Entities;
using MediatR;
using BookSiteProject.Application.Commands.CreateCategory;
using BookSiteProject.Application.Queries.GetAllCategoriesDto;

namespace BookSiteProject.MVC.Controllers
{
    public class CategoryController: Controller
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator; 
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _mediator.Send(new GetAllCategoriesDtoQuery());
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
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
