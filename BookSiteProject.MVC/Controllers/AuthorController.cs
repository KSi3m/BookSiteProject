using BookSiteProject.Application.Dtos;
using BookSiteProject.Application.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace BookSiteProject.MVC.Controllers
{
    public class AuthorController : Controller
    {
             private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var authors = await _authorService.GetAllAuthorsDto();
            return View(authors);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AuthorDto authorDto)
        {
            if (!ModelState.IsValid)
            {
                await Console.Out.WriteLineAsync("TtT");
                return View();
            }
            await _authorService.Create(authorDto);
            return RedirectToAction(nameof(Index));
        }

    }
}
