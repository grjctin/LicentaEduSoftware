namespace API.Helpers
{
    public class QuestionsParams : PaginationParams
    {
        public int CategoryId { get; set; } = 1;
        public int Difficulty { get; set; } = 3;
        public int AnswerType { get; set; } = 1;
        public string OrderBy { get; set; } = "dateAdded";
    }
}