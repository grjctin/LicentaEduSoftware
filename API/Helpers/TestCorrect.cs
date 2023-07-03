namespace API.Helpers
{
    public class TestCorrect
    {
        public int TestId { get; set; }
        public List<TestAnswer> Answers { get; set; }
    }

    public class TestAnswer
    {
        public int QuestionNumber { get; set; }
        public string GivenAnswer { get; set; }
    }
}