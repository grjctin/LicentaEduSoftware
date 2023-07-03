using API.Entities;
using API.Helpers;
using Microsoft.EntityFrameworkCore.Internal;

namespace API.Interfaces
{
    public interface ITestRepository
    {
        Task<int> CreateTest(int studentId, DateTime startDate, int duration);
        void AddQuestionToTest(int testId, int questionId, int questionNumber);
        Task<List<int>> GetTestQuestionIds(int testId);
        Task<Test> GetTest(int testId);
        Task<int> GetCorrectAnswer(int testId, int questionId);
        Task<int> GetQuestionNumber(int testId, int questionId);
        Task<bool> DbContainsTest(int testId);
        Task<int> CorrectTest(TestCorrect test);
        Task<bool> SaveAllAsync();
    }
}