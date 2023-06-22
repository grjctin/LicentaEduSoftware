namespace API.DTOs
{
    public class StudentsAttendanceDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<DateTime> PresenceDates { get; set; }
    }
}