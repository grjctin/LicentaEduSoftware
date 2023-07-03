using API.Data;
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
        private readonly IAnswerRepository _answerRepository;
        public QuestionsController(IQuestionRepository questionRepository, ICategoryRepository categoryRepository, IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
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

        [HttpPost]
        public async Task<ActionResult> AddQuestion(AddQuestionDTO addQuestion)
        {
            if (addQuestion.AnswerType == 1 && (addQuestion.Answer2 == null || addQuestion.Answer3 == null || addQuestion.Answer4 == null || addQuestion.CorrectAnswer == null))
                return BadRequest("There must be 4 answers");

            _questionRepository.AddQuestion(addQuestion);

            if (await _questionRepository.SaveAllAsync())
                return Ok("Question added successfully");

            return BadRequest("Something unexpected went wrong");
        }

        [HttpGet("paginated")]
        public async Task<ActionResult<QuestionsWithAnswersDTO>> GetPaginatedQuestions([FromQuery] QuestionsParams questionsParams)
        {
            Console.WriteLine("endpoint paginated" + ", orderBy=" + questionsParams.OrderBy + ", difficulty=" + questionsParams.Difficulty + "etc");
            var questions = await _questionRepository.GetPaginatedQuestions(questionsParams);
            Response.AddPaginationHeader(new PaginationHeader(
                questions.CurrentPage,
                questions.PageSize,
                questions.TotalCount,
                questions.TotalPages));

            List<Answer> answers = new List<Answer>();
            foreach(var question in questions){
                if(question.AnswerType == 1 && question.Id>=9)
                    answers.AddRange(await _answerRepository.GetAnswersByQuestionId(question.Id));
            }
            
            QuestionsWithAnswersDTO response = new QuestionsWithAnswersDTO 
            {
                Questions = questions,
                Answers = answers
            };
            Console.WriteLine(questions.Count + " " + answers.Count);

            return Ok(response);
        }

    }
}