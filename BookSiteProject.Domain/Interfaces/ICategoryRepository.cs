using BookSiteProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task Create(Category category);
        //void Create(Category category);
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetCategoryById(int? categoryId);
        //Task<Category?> GetCategoryByName(string name);
        Category? GetCategoryByName(string name);
    }
}
