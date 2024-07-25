using AutoMapper;
using BookSiteProject.Application.Commands.BookCommands.CreateBook;
using BookSiteProject.Application.Commands.BookCommands.DeleteBook;
using BookSiteProject.Application.Commands.BookOfferCommands.CreateBookOffer;
using BookSiteProject.Application.Dtos;
using BookSiteProject.Application.Queries.BookOfferQueries.GetBookOffersOfBook;
using BookSiteProject.Application.Queries.CategoryQueries.GetAllCategories;
using BookSiteProject.Application.Queries.GetAllAuthors;
using BookSiteProject.Application.Queries.GetAllBooks;
using BookSiteProject.Application.Queries.GetBookByEncodedName;
using BookSiteProject.Application.Services;
using BookSiteProject.Domain.Entities;
using BookSiteProject.MVC.Extensions;
using BookSiteProject.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookSiteProject.MVC.Controllers
{
    public class BookController: Controller
    {
        
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BookController(IMediator mediator, IMapper mapper)
        {
            _mediator= mediator;
            _mapper= mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _mediator.Send(new GetAllBooksQuery());
            return View(books);
        }

        [HttpGet]
        [Authorize(Roles = "Owner,Admin,Moderator")]
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
        [Authorize(Roles = "Owner,Admin,Moderator")]
        public async Task<IActionResult> Create(CreateBookCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            await _mediator.Send(command);

            this.SetNotifications(NotificationType.success, "Book created!: " + command.Title);

            return RedirectToAction(nameof(Index));
        }

        [Route("Book/{encodedName}/Details")]
        public async Task<IActionResult> Details(string encodedName)
        {
            var dto = await _mediator.Send(new GetBookByEncodedNameQuery(encodedName));
            return View(dto);
        }


        [Route("Book/{encodedName}/Edit")]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Edit(string encodedName)
        { 
            var dto = await _mediator.Send(new GetBookByEncodedNameQuery(encodedName));
            if(!dto.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }

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

            EditBookCommand model = _mapper.Map<EditBookCommand>(dto);
            return View(model);
        }

        [HttpPost]
        [Route("Book/{encodedName}/Edit")]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Edit(EditBookCommand command, string encodedName)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        //[Authorize(Roles = "Owner,Admin,Moderator")]
       // [Authorize]
        [Route("Book/BookOffer")]
        public async Task<IActionResult> CreateBookOffer(CreateBookOfferCommand command)
        {
            if (!ModelState.IsValid)
            {
                //this.SetNotifications(NotificationType.error, "Failed to add book offer!");
                return BadRequest(ModelState);
            }
            await _mediator.Send(command);


            //this.SetNotifications(NotificationType.success, "Book offer added!");
            return Ok();
        }

        [HttpGet]
        //[Authorize(Roles = "Owner,Admin,Moderator")]
        [Route("Book/{encodedName}/BookOffer")]
        public async Task<IActionResult> GetBookOffers(string encodedName)
        {
            var data = await _mediator.Send(new GetBookOffersOfBookQuery() { EncodedName = encodedName });
            return Ok(data);
        }
        [Route("Book/{encodedName}/Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string encodedName)
        {
            var dto = await _mediator.Send(new GetBookByEncodedNameQuery(encodedName));
            var model = _mapper.Map<DeleteBookCommand>(dto);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Book/{encodedName}/Delete")]
        public async Task<IActionResult> Delete(DeleteBookCommand command)
        {
            if (!ModelState.IsValid)
            {
                this.SetNotifications(NotificationType.error, "Failed to remove book!");
                return BadRequest(ModelState);
            }
        
            await _mediator.Send(command);


            //this.SetNotifications(NotificationType.success, "Book offer added!");
            return RedirectToAction(nameof(Index));
        }
    }
}
