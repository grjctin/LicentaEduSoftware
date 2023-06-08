using API.DTOs;
using API.Entities;
using API.Extensions;
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
            if (category == null)
                return NotFound();

            var questions = await _questionRepository.GetQuestionsByCategoryDifficulty(questionParams);
            if (questions == null)
                return NotFound();

            return Ok(questions);
        }

        // [HttpPost]
        // public async Task<ActionResult> AddQuestion(QuestionDto questionDto)
        // {
        //     //Cand adaug o intrebare, trebuie sa trimit si variantele de raspuns
        //     //Va trebui creat un Dto care sa contina raspunsuri
        //     return Ok("Not implemented yet");
        // }

        [HttpGet("paginated")]
        public async Task<ActionResult<PagedList<Question>>> GetPaginatedQuestions([FromQuery] QuestionsParams questionsParams)
        {
            Console.WriteLine("endpoint paginated" + ", orderBy=" + questionsParams.OrderBy + ", difficulty=" + questionsParams.Difficulty + "etc");
            var questions = await _questionRepository.GetPaginatedQuestions(questionsParams);
            Response.AddPaginationHeader(new PaginationHeader(
                questions.CurrentPage,
                questions.PageSize,
                questions.TotalCount,
                questions.TotalPages));

            return Ok(questions);
        }
    }
}