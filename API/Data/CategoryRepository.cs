using System.Diagnostics;
using System.Reflection;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        //De modificat cu CreateCategoryDto
        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
        }

        public void DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
        }

        public async Task<List<Category>> GetCategories()
        {        
            return await _context.Categories.ToListAsync();
        }

        public async Task<List<Category>> GetCategoriesByClass(int classNumber)
        {
            return await _context.Categories.Where(cat => cat.ClassNumber == classNumber).ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            return await _context.Categories.Where(cat => cat.Name == name).SingleOrDefaultAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}