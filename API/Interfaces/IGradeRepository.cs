using API.Entities;

namespace API.Interfaces
{
    public interface IGradeRepository
    {
         void AddGrade(Grade grade);
         void AddCategoryGrade(CategoryGrade categoryGrade);
         Task<bool> SaveAllAsync();

    }
}