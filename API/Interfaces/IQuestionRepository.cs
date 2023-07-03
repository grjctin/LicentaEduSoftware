using API.DTOs;
using API.Entities;
using API.Helpers;
using Microsoft.EntityFrameworkCore.Internal;

namespace API.Interfaces
{
    public interface IQuestionRepository
    {
        Task<List<Question>> GetQuestions();
        Task<List<Question>> GetQuestionsByCategoryId(int categoryId);
        Task<List<Question>> GetQuestionsByCategoryDifficulty(QuestionParams questionParams);
        Task<Question> GetQuestionById(int id);
        //Task<List<QuestionWithAnswer>> GetQuestionsForTest(QuestionConfig questionConfig);
        void AddQuestion(AddQuestionDTO addQuestion);
        void DeleteQuestion(Question question);
        Task<List<Question>> GetQuestionsByClassNumber(int classNumber);
        Task<PagedList<Question>> GetPaginatedQuestions(QuestionsParams questionsParams);
        Task<bool> SaveAllAsync();
        Task<List<Answer>> GetQuestionAnswers(int questionId);
        Task<List<int>> GetQuestionIdsForConfiguration(QuestionConfig questionConfig);

        //Task<List<TestQuestion>> GetTestQuestions(List<int> questionIds);
    }
}