using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace API.Data
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly DataContext _context;

        public QuestionRepository(DataContext context)
        {
            _context = context;
        }

        public void AddQuestion(AddQuestionDTO addQuestion)
        {
            Question question = new Question
            {
                CategoryId = addQuestion.CategoryId,
                difficulty = addQuestion.Difficulty,
                AnswerType = addQuestion.AnswerType,
                Text = addQuestion.Text,
                DateAdded = addQuestion.DateAdded
            };
            if (addQuestion.AnswerType == 1) //grila
            {
                _context.Questions.Add(question);
                _context.SaveChanges();
                //salvez modificarile pentru ca entity framework sa populeze campul Id din obiectul question
                //am nevoie de id pentru adaugarea raspunsurilor
                Answer correctAnswer = new Answer
                {
                    QuestionId = question.Id,
                    Text = addQuestion.CorrectAnswer,
                    IsCorrect = true
                };
                _context.Answers.Add(correctAnswer);
                Answer ans2 = new Answer
                {
                    QuestionId = question.Id,
                    Text = addQuestion.Answer2,
                    IsCorrect = false
                };
                _context.Answers.Add(ans2);
                Answer ans3 = new Answer
                {
                    QuestionId = question.Id,
                    Text = addQuestion.Answer3,
                    IsCorrect = false
                };
                _context.Answers.Add(ans3);
                Answer ans4 = new Answer
                {
                    QuestionId = question.Id,
                    Text = addQuestion.Answer4,
                    IsCorrect = false
                };
                _context.Answers.Add(ans4);

            }
            else
            {
                _context.Questions.Add(question);
            }
        }

        public void DeleteQuestion(Question question)
        {
            _context.Questions.Remove(question);
        }

        public async Task<Question> GetQuestionById(int id)
        {
            return await _context.Questions.FindAsync(id);
        }

        public async Task<List<Question>> GetQuestions()
        {
            return await _context.Questions.ToListAsync();
        }

        public async Task<List<Question>> GetQuestionsByCategoryDifficulty(QuestionParams questionParams)
        {
            return await _context.Questions.Where(q =>
                q.CategoryId == questionParams.CategoryId &&
                q.difficulty == questionParams.Difficulty)
                .ToListAsync();
        }

        public async Task<List<Question>> GetQuestionsByCategoryId(int categoryId)
        {
            return await _context.Questions.Where(q => q.CategoryId == categoryId).ToListAsync();
        }

        public async Task<List<Question>> GetQuestionsByClassNumber(int classNumber)
        {
            var classNumberCategories = await _context.Categories
                .Where(cat => cat.ClassNumber == classNumber)
                .Select(cat => cat.Id)
                .ToListAsync();
            return await _context.Questions
                .Where(q => classNumberCategories.Contains(q.CategoryId))
                .ToListAsync();
        }


        public async Task<PagedList<Question>> GetPaginatedQuestions(QuestionsParams questionsParams)
        {
            Console.WriteLine("Repository paginated questions questionsParams.difficulty = " + questionsParams.Difficulty);
            var query = _context.Questions.AsQueryable();

            if (questionsParams.Difficulty != 4)
            {
                query = query.Where(question => question.CategoryId == questionsParams.CategoryId);
                query = query.Where(q => q.difficulty == questionsParams.Difficulty);
                query = query.Where(q => q.AnswerType == questionsParams.AnswerType);
            }

            query = questionsParams.OrderBy switch
            {
                "difDescending" => query.OrderByDescending(q => q.difficulty),
                "difAscending" => query.OrderBy(q => q.difficulty),
                _ => query.OrderBy(q => q.DateAdded)
            };

            return await PagedList<Question>.CreateAsync(
                query.AsNoTracking(),
                questionsParams.PageNumber,
                questionsParams.PageSize);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<int>> GetQuestionIdsForConfiguration(QuestionConfig questionConfig)
        {
            //returneaza nrOfQuestions intrebari random de o anumita configuratie
            var allQuestionIds = await _context.Questions
                .Where(q =>
                q.CategoryId == questionConfig.CategoryId &&
                q.difficulty == questionConfig.Difficulty &&
                q.AnswerType == questionConfig.AnswerType)
                .Select(q => q.Id)
                .ToListAsync();

            Random random = new Random();
            List<int> selectedQuestionIds = new List<int>();
            int nrQuestions = Math.Min(questionConfig.NumberOfQuestions, allQuestionIds.Count);
            while (selectedQuestionIds.Count < nrQuestions)
            {
                int randomNr = random.Next(0, allQuestionIds.Count);
                if (!selectedQuestionIds.Contains(allQuestionIds[randomNr]))
                {
                    selectedQuestionIds.Add(allQuestionIds[randomNr]);
                }
            }
            return selectedQuestionIds;
        }

        public async Task<List<Answer>> GetQuestionAnswers(int questionId)
        {
            return await _context.Answers.Where(a => a.QuestionId == questionId).ToListAsync();
        }

        // public async Task<List<TestQuestion>> GetTestQuestions(List<int> questionIds)
        // {
        //     List<TestQuestion> response = new List<TestQuestion>();

        //     foreach(var id in questionIds)
        //     {
        //         var question = GetQuestionById(id);
        //         var answers = GetQuestionAnswers(id);
        //     }
        // }

        // public async Task<List<QuestionWithAnswer>> GetQuestionsForTest(QuestionConfig questionConfig)
        // {
        //     List<QuestionWithAnswer> questions = new List<QuestionWithAnswer>();

        //     var questionIds = await this.GetQuestionIdsForConfiguration(questionConfig);

        //     foreach(int questionId in questionIds){
        //         List<Answer> answers = await GetQuestionAnswers(questionId);
        //         QuestionWithAnswer q = new QuestionWithAnswer{QuestionId = questionId,Answers = answers};
        //         questions.Add(q);
        //     }

        //     return questions;
        // }
    }
}