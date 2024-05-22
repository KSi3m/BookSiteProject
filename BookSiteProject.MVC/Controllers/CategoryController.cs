using BookSiteProject.Application.Dtos;
using BookSiteProject.Application.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using BookSiteProject.Domain.Entities;

namespace BookSiteProject.MVC.Controllers
{
    public class CategoryController: Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesDto();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto category)
        {
            if (!ModelState.IsValid)
            {
                await Console.Out.WriteLineAsync("TtT");
                return View();
            }
            await _categoryService.Create(category);
            return RedirectToAction(nameof(Index));
        }
    }
}
