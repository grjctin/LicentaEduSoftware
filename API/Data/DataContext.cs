using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<SchoolClass> Classes { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<CategoryGrade> CategoryGrades { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestHasQuestion> TestHasQuestion { get; set; }
        public DbSet<TestTaken> TestTaken { get; set; }
        
    }
}