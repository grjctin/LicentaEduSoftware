namespace API.Helpers
{
    public class TestParams
    {
        public List<QuestionConfig> QuestionConfigurations { get; set; }
        public List<int>  StudentIds { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
    }
}