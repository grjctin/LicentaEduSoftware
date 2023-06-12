namespace API.DTOs
{
    public class StudentsGradesDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<int> Grades { get; set; }
        public Dictionary<int, List<int>> CategoryGrades { get; set; }
    }
}