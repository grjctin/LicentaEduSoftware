using API.Entities;

namespace API.Helpers
{
    public class QuestionWithAnswer
    {
        public int QuestionId { get; set; }
        public List<Answer> Answers { get; set; }
    }
}