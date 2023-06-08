using API.Entities;

namespace API.Interfaces
{
    public interface ISchoolClassRepository
    {
         Task<List<SchoolClass>> GetSchoolClasses();
         Task<List<SchoolClass>> GetSchoolClassesByProfessorId(int professorId);
         Task<List<SchoolClass>> GetSchoolClassesByClassNumber(int classNumber);
         void AddSchoolClass(SchoolClass schoolClass);
         Task<bool> SaveAllAsync();
         Task<bool> ClassNameExists(string className);
    }
}