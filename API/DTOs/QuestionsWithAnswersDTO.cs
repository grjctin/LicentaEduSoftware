using API.Entities;
using API.Helpers;

namespace API.DTOs
{
    public class QuestionsWithAnswersDTO
    {
        public PagedList<Question> Questions { get; set; }
        public List<Answer> Answers { get; set; }
    }
}