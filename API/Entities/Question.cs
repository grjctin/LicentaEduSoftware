namespace API.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int difficulty { get; set; }
        public string Text { get; set; }
        public int AnswerType { get; set; }
        public DateTime DateAdded { get; set; }
        
        // public bool HasPhoto { get; set; }
        // public Photo Photo { get; set; }
    }
}