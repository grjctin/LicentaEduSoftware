using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly DataContext _context;

        public QuestionRepository(DataContext context)
        {
            _context = context;

        }

        public void AddQuestion(Question question)
        {
            _context.Questions.Add(question);
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
    }
}