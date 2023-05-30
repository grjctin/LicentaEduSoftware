using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
        Task<List<Category>> GetCategoriesByClass(int classNumber);
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> GetCategoryByNameAsync(string name);
        void AddCategory(Category category);
        void DeleteCategory(Category category);
        Task<bool> SaveAllAsync();
    }
}