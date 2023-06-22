using System.ComponentModel;

namespace API.Entities
{
    public class Test
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }        
    }
}