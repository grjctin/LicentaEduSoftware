using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CategoriesController : BaseApiController
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            return await _categoryRepository.GetCategories();
        }

        [HttpGet("{classNumber}")]
        public async Task<ActionResult<List<Category>>> GetCategoriesByClass(int classNumber)
        {
            return await _categoryRepository.GetCategoriesByClass(classNumber);
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory(CreateCategoryDto categoryDto)
        {
            //Console.WriteLine(categoryDto.ClassNumber + " " + categoryDto.CategoryName);
            var category = new Category
            {
                ClassNumber = categoryDto.ClassNumber,
                Name = categoryDto.CategoryName
            };
            _categoryRepository.AddCategory(category);

            if(await _categoryRepository.SaveAllAsync())
                return Ok("Category added successfully");
            return BadRequest("Failed to add category");
        }

        [HttpGet("name={name}")]
        public async Task<ActionResult<Category>> GetCategoryByName(string name)
        {
            return await _categoryRepository.GetCategoryByNameAsync(name);
        }

        [HttpGet("id={id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            return await _categoryRepository.GetCategoryByIdAsync(id);
        }
    }
}