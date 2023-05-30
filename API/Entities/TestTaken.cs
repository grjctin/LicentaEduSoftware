namespace API.Entities
{
    public class TestTaken
    {
        public int Id { get; set; }
        public int TestHasQuestionId { get; set; }
        public TestHasQuestion TestQuestion { get; set; }
        public string GivenAnswer { get; set; }
    }
}