namespace API.Entities
{
    public class TestHasQuestion
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public string CorrectAnswer { get; set; }
        public int QuestionNumber { get; set; }
    }
}