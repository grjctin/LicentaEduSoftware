using System.ComponentModel;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TestsController : BaseApiController
    {
        private readonly ITestRepository _testRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IGradeRepository _gradesRepository;

        public TestsController(ITestRepository testRepository, IQuestionRepository questionRepository, IStudentRepository studentRepository, IGradeRepository gradesRepository)
        {
            _gradesRepository = gradesRepository;
            _studentRepository = studentRepository;
            _questionRepository = questionRepository;
            _testRepository = testRepository;

        }

        [HttpPost("correct")]
        public async Task<ActionResult<int>> CorrectTest(TestCorrect test)
        {

            if (!await _testRepository.DbContainsTest(test.TestId))
                return BadRequest();

            int grade = await _testRepository.CorrectTest(test);

            _gradesRepository.AddTestGrade(test.TestId, grade);

            return Ok(grade);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTests(TestParams testParams)
        {
            if (testParams.QuestionConfigurations.Count == 0) return BadRequest("No configurations selected");
            if (testParams.StudentIds.Count == 0) return BadRequest("No students selected");

            foreach (int s in testParams.StudentIds)
            {
                int testId = await _testRepository.CreateTest(s, testParams.StartDate, testParams.Duration);
                int questionNumber = 1;

                foreach (var config in testParams.QuestionConfigurations)
                {
                    var questionIds = await _questionRepository.GetQuestionIdsForConfiguration(config);

                    foreach (var questionId in questionIds)
                    {
                        _testRepository.AddQuestionToTest(testId, questionId, questionNumber);
                        questionNumber++;
                    }
                }
            }
            if (await _testRepository.SaveAllAsync()) return Ok("Tests added successfully");
            else return BadRequest("something went wrong");
        }

        [HttpGet("testId={testId}")]
        public async Task<ActionResult<TestDTO>> GetTestView(int testId)
        {
            TestDTO response = new TestDTO();

            if (!await _testRepository.DbContainsTest(testId))
                return BadRequest("Selected Test doesn't exist");

            Test test = await _testRepository.GetTest(testId);
            response.Duration = test.Duration;
            response.StartDate = test.StartDate;
            response.StudentName = await _studentRepository.GetStudentNameById(test.StudentId);
            response.Questions = new List<TestQuestion>();

            var questionIds = await _testRepository.GetTestQuestionIds(testId);
            // response.Questions = await _questionRepository.GetTestQuestions(questionIds);
            foreach (var id in questionIds)
            {
                TestQuestion t = new TestQuestion();
                t.QuestionId = id;

                var question = await _questionRepository.GetQuestionById(id);
                t.QuestionText = question.Text;
                t.AnswerType = question.AnswerType;
                t.QuestionNumber = await _testRepository.GetQuestionNumber(testId, id);
                t.CorrectAnswer = await _testRepository.GetCorrectAnswer(testId, id);

                if (question.AnswerType == 1)
                {
                    var answers = await _questionRepository.GetQuestionAnswers(id);
                    answers = ShuffleArray(answers, t.CorrectAnswer);
                    //t.Answers = answers;
                    t.Answers = answers.Select(a => a.Text).ToList();
                }
                response.Questions.Add(t);
            }
            return Ok(response);
        }

        public List<Answer> ShuffleArray(List<Answer> array, int correctAnswerIndex)
        {
            Random random = new Random();
            for (int i = array.Count - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                Answer temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }

            Answer correctAnswer = array.FirstOrDefault(item => item.IsCorrect);
            if (correctAnswer != null)
            {
                array.Remove(correctAnswer);
                array.Insert(correctAnswerIndex, correctAnswer);
            }
            return array;
        }
    }


}