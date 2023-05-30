namespace API.Entities
{
    public class Student
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SchoolClassId { get; set; }
        public SchoolClass SchoolClass { get; set; }
        public string Cnp { get; set; }
        public string Email { get; set; }   
        public string PhoneNumber { get; set; }
    }
}