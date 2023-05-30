namespace API.Entities
{
    public class CategoryGrade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int Grade { get; set; }
    }
}