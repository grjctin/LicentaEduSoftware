using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _context;
        public StudentRepository(DataContext context)
        {
            _context = context;

        }
        public void AddStudent(Student student)
        {
            _context.Students.Add(student);
        }

        public void DeleteStudent(Student student)
        {
            _context.Students.Remove(student);
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<Student> GetStudentByName(string firstname, string lastname)
        {
            firstname = firstname.ToLower();
            lastname = lastname.ToLower();
            return await _context.Students.Where(s => (s.FirstName.ToLower() == firstname 
                && s.LastName.ToLower() == lastname) 
                || (s.FirstName.ToLower() == lastname 
                && s.LastName.ToLower() == firstname)
                ).FirstOrDefaultAsync();
        }

        public async Task<List<Student>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<List<Student>> GetStudentsByClassId(int id)
        {
            return await _context.Students.Where(s => s.SchoolClassId == id).ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateStudent(Student student)
        {
            _context.Entry(student).State = EntityState.Modified;
        }

        
    }
}