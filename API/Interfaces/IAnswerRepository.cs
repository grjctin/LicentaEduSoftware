using API.Entities;

namespace API.Interfaces
{
    public interface IAnswerRepository
    {
         Task<List<Answer>> GetAnswersByQuestionId(int questionId);
    }
}