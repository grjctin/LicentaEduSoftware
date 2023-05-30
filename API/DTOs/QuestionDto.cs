namespace API.DTOs
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int Difficulty { get; set; }
        public string Text { get; set; }
        public int AnswerType { get; set; }
    }
}