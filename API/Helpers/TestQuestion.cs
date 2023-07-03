using API.Entities;

namespace API.Helpers
{
    public class TestQuestion
    {
       public int QuestionId { get; set; }
       public string QuestionText { get; set; }
       public int AnswerType { get; set; }
       public int QuestionNumber { get; set; }
       public int CorrectAnswer { get; set; }
       #nullable enable
       //public List<Answer>? Answers { get; set; }
       public List<string>? Answers { get; set; }
    }
}