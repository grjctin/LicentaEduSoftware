namespace API.Entities
{
    public class SchoolClass
    {
        public int Id  { get; set; }
        public int ProfessorId { get; set; }
        public AppUser Professor { get; set; }
        public int ClassNumber { get; set; }
        public string ClassName { get; set; }
    }
}