using API.Helpers;

namespace API.DTOs
{
    public class TestDTO
    {
        public string StudentName { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public List<TestQuestion> Questions { get; set; }
    }
}