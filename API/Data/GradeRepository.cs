using API.Entities;
using API.Interfaces;

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
            throw new NotImplementedException();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}