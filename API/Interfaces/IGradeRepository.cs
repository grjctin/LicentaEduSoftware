using API.Entities;

namespace API.Interfaces
{
    public interface IGradeRepository
    {
         void AddGrade(Grade grade);
         void AddCategoryGrade(CategoryGrade categoryGrade);
         void AddTestGrade(int testId, int grade);
         Task<bool> SaveAllAsync();

    }
}