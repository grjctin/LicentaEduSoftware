namespace API.DTOs
{
    public class AddQuestionDTO
    {
        public int CategoryId { get; set; }
        public int Difficulty { get; set; }
        public int AnswerType { get; set; }
        public string Text { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;

        #nullable enable
        public string? CorrectAnswer { get; set; }
        public string? Answer2 { get; set; }
        public string? Answer3 { get; set; }
        public string? Answer4 { get; set; }
    }
}

