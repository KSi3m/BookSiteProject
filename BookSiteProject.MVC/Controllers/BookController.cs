using BookSiteProject.Application.Dtos;
using BookSiteProject.Application.Services;
using BookSiteProject.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookSiteProject.MVC.Controllers
{
    public class BookController: Controller
    {
        private readonly IBookService _bookservice;
        private readonly ICategoryService _categoryService;
        private readonly IAuthorService _authorService;

        public BookController(IBookService bookservice, ICategoryService categoryService, IAuthorService authorService)
        {
            _bookservice = bookservice;
            _categoryService = categoryService;
            _authorService = authorService;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _bookservice.GetAll();
            return View(books);
        }

            [HttpGet]
        public async Task<IActionResult> Create()
        {
            var cats = await _categoryService.GetAll();
  
            var authors = await _authorService.GetAuthors();
            var authorList = authors.Select(a => new SelectListItem 
            { 
                Value = a.Id.ToString(), 
                Text = a.Firstname + " " + a.Surname 
            }).ToList();
            ViewBag.Authors = authorList;
   
            var categoryItems = cats.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(), 
                Text = c.Name 
            }).ToList();

            ViewBag.Categories = categoryItems;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookDto book)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach(var x in errors)
            {
                await Console.Out.WriteLineAsync(x.ErrorMessage);
            }
            if (!ModelState.IsValid)
            {
                await Console.Out.WriteLineAsync("TtT");
                return View();
            }
           
            await _bookservice.Create(book);
            return RedirectToAction(nameof(Index));
        }

    }
}
