using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IQuestionRepository
    {
        Task<List<Question>> GetQuestions();
        Task<List<Question>> GetQuestionsByCategoryId(int categoryId);
        Task<List<Question>> GetQuestionsByCategoryDifficulty(QuestionParams questionParams);
        Task<Question> GetQuestionById(int id);
        //Task<Question> GetQuestionByNameAsync(string name);
        void AddQuestion(Question question);
        void DeleteQuestion(Question question);
        Task<List<Question>> GetQuestionsByClassNumber(int classNumber);
        Task<bool> SaveAllAsync();
    }
}