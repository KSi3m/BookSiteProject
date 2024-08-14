using BookSiteProject.Application.Commands.CategoryCommands.ChangeStatusOfCategory;
using BookSiteProject.Application.Commands.CategoryCommands.CreateCategory;
using BookSiteProject.Application.Commands.CategoryCommands.DeleteCategory;
using BookSiteProject.Application.Commands.CategoryCommands.EditCategory;
using BookSiteProject.Application.Queries.CategoryQueries.GetAllCategoriesDto;
using BookSiteProject.Application.Queries.CategoryQueries.GetCategoryByName;
using BookSiteProject.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookSiteProject.MVC.Controllers.Api
{

    [ApiController]
    [Route("api/")]
    public class CategoryController: ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        [Route("categories/{name}")]
        public async Task<IActionResult> GetCategories(string name)
        {
            var category = await _mediator.Send(new GetCategoryByNameQuery(name));
            return Ok(category);

        }
        [HttpGet]
        [Authorize]
        [Route("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _mediator.Send(new GetAllCategoriesDtoQuery());
            return Ok(categories);

        }


        [HttpPost]
        [Authorize]
        [Route("categories")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> Create([FromForm] CreateCategoryCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _mediator.Send(command);
            return Created();
        }

        [HttpPut]
        [Authorize]
        [Route("categories/{name}")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> Edit([FromForm] EditCategoryCommand command, string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route("categories/{name}")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> Delete(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _mediator.Send(new DeleteCategoryCommand() { CategoryName=name});
            return NoContent();
        }


        [HttpPatch]
        [Authorize]
        [Route("categories/{name}")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> ChangeStatus([FromForm] ChangeStatusOfCategoryCommand command, string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            await _mediator.Send(command);
            return Ok();
        }


    }
}
