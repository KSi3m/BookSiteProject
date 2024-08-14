using BookSiteProject.Application.Dtos;
using BookSiteProject.Application.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using BookSiteProject.Domain.Entities;
using MediatR;
using BookSiteProject.Application.Queries.CategoryQueries.GetAllCategoriesDto;
using BookSiteProject.Application.Queries.CategoryQueries.GetCategoryByName;
using Microsoft.AspNetCore.Authorization;
using BookSiteProject.MVC.Extensions;
using BookSiteProject.MVC.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

using BookSiteProject.Application.Commands.CategoryCommands.ChangeStatusOfCategory;
using BookSiteProject.Application.Commands.CategoryCommands.DeleteCategory;
using BookSiteProject.Application.Commands.BookOfferCommands.EditBookOffer;
using BookSiteProject.Application.Queries.BookOfferQueries.GetBookOfferById;
using BookSiteProject.Application.Commands.CategoryCommands.CreateCategory;
using BookSiteProject.Application.Commands.CategoryCommands.EditCategory;

namespace BookSiteProject.MVC.Controllers.MVC
{
    public class CategoryController : Controller
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
        [Route("Category/{name}")]
        public async Task<IActionResult> Details(string name)
        {
            var dto = await _mediator.Send(new GetCategoryByNameQuery(name));
            return View(dto);
        }


    }
}
