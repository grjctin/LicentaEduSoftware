using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace API.Data
{
    public class TestRepository : ITestRepository
    {
        private readonly DataContext _context;
        public TestRepository(DataContext context)
        {
            _context = context;

        }

        

        public async Task<int> CreateTest(int studentId, DateTime startDate, int duration)
        {
            Test test = new Test
            {
                StudentId = studentId,
                StartDate = startDate,
                Duration = duration
            };

            _context.Tests.Add(test);
            await _context.SaveChangesAsync();

            return test.Id;
        }



        public async void AddQuestionToTest(int testId, int questionId, int questionNumber)
        {
            int answerType = await _context.Questions.Where(q=> q.Id ==questionId).Select(q => q.AnswerType).FirstAsync();

            Random random = new Random();

            int correctAnswer = random.Next(0,4);
            
            TestHasQuestion t = new TestHasQuestion{
                TestId = testId,
                QuestionId = questionId,
                CorrectAnswer = getCorrespondingLetter(correctAnswer),
                QuestionNumber = questionNumber
            };

            if(answerType == 2)
                t.CorrectAnswer = "openAnswer";

            _context.TestHasQuestion.Add(t);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public string getCorrespondingLetter(int index)
        {
            switch(index) 
            {
                case 0: 
                    return "a";
                case 1:
                    return "b";
                case 2:
                    return "c";
                case 3:
                    return "d";
                default:
                    Console.WriteLine("Wrong index " + index);
                    return "WrongAnswer";
            }
        }


        public async Task<List<int>> GetTestQuestionIds(int testId)
        {
            var questionIds = await _context.TestHasQuestion
                .Where(t => t.TestId == testId)
                .Select(t => t.QuestionId)
                .ToListAsync(); 

            return questionIds;
        }

        public async Task<int> GetCorrectAnswer(int testId, int questionId)
        {
            string correctAnswer = await _context.TestHasQuestion
                .Where(t => 
                t.TestId == testId &&
                t.QuestionId == questionId)
                .Select(t => t.CorrectAnswer).FirstAsync();

            switch(correctAnswer)
            {
                case "a":
                    return 0;
                case "b":
                    return 1;
                case "c":
                    return 2;
                case "d":
                    return 3;
                default:
                    return -1;
            }
        }

        public async Task<int> GetQuestionNumber(int testId, int questionId)
        {
            return await _context.TestHasQuestion
                .Where(t => 
                t.TestId == testId &&
                t.QuestionId == questionId)
                .Select(t => t.QuestionNumber).FirstAsync();
        }

        public async Task<Test> GetTest(int testId)
        {
            return await _context.Tests.FindAsync(testId);
        }

        public async Task<bool> DbContainsTest(int testId)
        {
            return await _context.Tests.AnyAsync(t => t.Id == testId);
        }

        public async Task<int> CorrectTest(TestCorrect test)
        {
            double grade=0;
            double numberOfQuestions = test.Answers.Count;
            foreach(var answer in test.Answers)
            {

                string correctAnswer = await _context.TestHasQuestion
                    .Where(t =>
                    t.TestId == test.TestId &&
                    t.QuestionNumber == answer.QuestionNumber)
                    .Select(t => t.CorrectAnswer)
                    .FirstAsync();

                if(correctAnswer.Length > 2)
                {
                    double points = double.Parse(answer.GivenAnswer);
                    grade += 10/numberOfQuestions*points;
                }
                else if(correctAnswer == answer.GivenAnswer)
                {
                    grade += 10/numberOfQuestions;
                }
            }
            if(grade == 0)
                return 1;

            return (int)Math.Round(grade, MidpointRounding.ToEven);
        }
    }
}