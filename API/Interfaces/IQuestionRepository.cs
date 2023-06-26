using API.DTOs;
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
        void AddQuestion(AddQuestionDTO addQuestion);
        void DeleteQuestion(Question question);
        Task<List<Question>> GetQuestionsByClassNumber(int classNumber);
        Task<PagedList<Question>> GetPaginatedQuestions(QuestionsParams questionsParams);
        Task<bool> SaveAllAsync();
    }
}