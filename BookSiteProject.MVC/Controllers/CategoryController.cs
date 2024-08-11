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
        [Authorize]
        [Route("/api/Category/{name}")]
        public async Task<IActionResult> GetCategories(string name)
        {
            var category = await _mediator.Send(new GetCategoryByNameQuery(name));
            //var model = _mapper.Map<EditBookOfferCommand>(offer);
            return Ok(category);

        }

        [HttpGet]
        [Authorize]
        [Route("/api/Categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _mediator.Send(new GetAllCategoriesDtoQuery());
            //var model = _mapper.Map<EditBookOfferCommand>(offer);
            return Ok(categories);

        }


        [HttpPost]
        [Authorize]
        [Route("api/Category/Create")]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("api/Category/Edit")]
        public async Task<IActionResult> Edit(EditCategoryCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        [Route("Category/{name}")]
        public async Task<IActionResult> Details(string name)
        {
            var dto = await _mediator.Send(new GetCategoryByNameQuery(name));
            return View(dto);
        }

        [HttpPut]
        [Authorize]
        [Route("Category/{name}")]
        public async Task<IActionResult> Edit(string name)
        {
            var dto = await _mediator.Send(new GetCategoryByNameQuery(name));
            return View(dto);
        }

        [HttpDelete]
        [Authorize]
        [Route("Category/Delete")]
        public async Task<IActionResult> Delete(DeleteCategoryCommand command)
        {
            if (!ModelState.IsValid)
            {
               // this.SetNotifications(NotificationType.error, "Failed to delete category!");
                return BadRequest(ModelState);
            }

            await _mediator.Send(command);


            //this.SetNotifications(NotificationType.success, "Category Deleted");
            return Ok();
        }

        
        [HttpPost]
        [Authorize]
        [Route("api/Category/ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(ChangeStatusOfCategoryCommand command)
        {
            if (!ModelState.IsValid)
            {
                var text = "";
                if(command.Status) text = "Failed to activate category!";
                else text = "Failed to deactivate category!";

                this.SetNotifications(NotificationType.error, "Failed to unlist category!");
                return BadRequest(ModelState);
            }

            await _mediator.Send(command);


            this.SetNotifications(NotificationType.success, "Operation was succesfull");
            return Ok();
        }

       

    }
}
