using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class QuestionsController : BaseApiController
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ICategoryRepository _categoryRepository;
        public QuestionsController(IQuestionRepository questionRepository, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _questionRepository = questionRepository;

        }

        [HttpGet("class={classNumber}")]
        public async Task<ActionResult<List<Question>>> GetQuestionsByClassNumber(int classNumber)
        {
            return await _questionRepository.GetQuestionsByClassNumber(classNumber);
        }

        [HttpGet]
        public async Task<ActionResult<List<Question>>> GetQuestions()
        {
            return await _questionRepository.GetQuestions();
        }

        [HttpGet("categoryId={categoryId}")]
        public async Task<ActionResult<List<Question>>> GetQuestionsByCategoryId(int categoryId)
        {
            return await _questionRepository.GetQuestionsByCategoryId(categoryId);
        }

        [HttpGet("category-difficulty/")]
        public async Task<ActionResult<List<Question>>> GetQuestionsByCategoryDifficulty([FromQuery] QuestionParams questionParams)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(questionParams.CategoryId);
            if(category == null)
                return NotFound();
            
            var questions = await _questionRepository.GetQuestionsByCategoryDifficulty(questionParams);
            if(questions == null)
                return NotFound();

            return Ok(questions);
        }
    }
}