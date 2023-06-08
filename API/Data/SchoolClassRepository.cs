using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class SchoolClassRepository : ISchoolClassRepository
    {
        private readonly DataContext _context;
        public SchoolClassRepository(DataContext context)
        {
            _context = context;

        }
        public void AddSchoolClass(SchoolClass schoolClass)
        {
            _context.Classes.Add(schoolClass);
        }

        public async Task<bool> ClassNameExists(string className)
        {
            var classNames = await _context.Classes.Select(c => c.ClassName).ToListAsync();
            return classNames.Contains(className);
        }

        public async Task<List<SchoolClass>> GetSchoolClasses()
        {
            return await _context.Classes.ToListAsync();
        }

        public async Task<List<SchoolClass>> GetSchoolClassesByClassNumber(int classNumber)
        {
            return await _context.Classes.Where(c => c.ClassNumber == classNumber).ToListAsync();
        }

        public async Task<List<SchoolClass>> GetSchoolClassesByProfessorId(int professorId)
        {
            return await _context.Classes.Where(c => c.ProfessorId == professorId).ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}