﻿using BookSiteProject.Domain.Entities;
using BookSiteProject.Domain.Interfaces;
using BookSiteProject.Infrastructure.Migrations;
using BookSiteProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Infrastructure.Repostories
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly BookSiteProjectDbContext _dbcontext;

        public CategoryRepository(BookSiteProjectDbContext dbcontext)
        {
            this._dbcontext = dbcontext;
        }
        public async Task Create(Category category)
        {
            _dbcontext.Add(category);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _dbcontext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int? categoryId)
        {
           return await _dbcontext.Categories.FindAsync(categoryId);
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            var test = await _dbcontext.Categories
                .FirstOrDefaultAsync(x => x.Name != null && x.Name.ToLower() == name.ToLower());
            return test;
        } 
        /*public Category? GetCategoryByName(string name)
        {
            if (name == null)
            {
                return null;
            }
            var test =  _dbcontext.Categories
                .FirstOrDefault(x => x.Name != null && x.Name.ToLower() == name.ToLower());
            return test;
        }*/
    }
}
