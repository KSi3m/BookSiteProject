using BookSiteProject.Application.Dtos;
using BookSiteProject.Domain.Entities;

namespace BookSiteProject.Application.Services
{
    public interface ICategoryService
    {
        Task Create(CategoryDto category);
        //void Create(CategoryDto category);
        Task<IEnumerable<Category>> GetAll(); 
        Task<IEnumerable<CategoryDto>> GetAllCategoriesDto(); 
    }
}