using API.Entities;
using API.Helpers;
using API.Interfaces;
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
            var classNumberCategories = _context.Categories
                .Where(cat => cat.ClassNumber == classNumber)
                .Select(cat => cat.Id)
                .ToList();
            return await _context.Questions
                .Where(q => classNumberCategories.Contains(q.CategoryId))
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}