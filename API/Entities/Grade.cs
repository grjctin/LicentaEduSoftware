namespace API.Entities
{
    public class Grade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
        public int TestGrade { get; set; }
        public DateTime DateReceived { get; set; }

    }
}