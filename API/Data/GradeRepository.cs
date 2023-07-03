using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class GradeRepository : IGradeRepository
    {
        private readonly DataContext _context;

        public GradeRepository(DataContext context)
        {
            _context = context;
        }

        public void AddCategoryGrade(CategoryGrade categoryGrade)
        {
            _context.CategoryGrades.Add(categoryGrade);
        }

        public void AddGrade(Grade grade)
        {
            _context.Grades.Add(grade);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async void AddTestGrade(int testId, int grade)
        {
            int studentId = await _context.Tests.Where(t => t.Id == testId).Select(t => t.StudentId).FirstAsync();
            var gradeToAdd = new Grade{
                StudentId = studentId,
                TestId = testId,
                TestGrade = grade,
                DateReceived = DateTime.Now
            };
            _context.Grades.Add(gradeToAdd);
            await SaveAllAsync();
        }
    }
}