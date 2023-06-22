using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

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

        public async Task<List<Student>> GetStudentsDetailsByClassId(int id)
        {
            return await _context.Students.Where(s => s.SchoolClassId == id).ToListAsync();
        }

        public async Task<List<StudentsGradesDto>> GetStudentsGradesByClassId(int id)
        {
            var students = await _context.Students.Where(s => s.SchoolClassId == id).ToListAsync();
            List<StudentsGradesDto> studentsGrades = new List<StudentsGradesDto>();
            foreach (var s in students)
            {
                var sGrades = new StudentsGradesDto();
                sGrades.Id = s.Id;
                sGrades.FirstName = s.FirstName;
                sGrades.LastName = s.LastName;
                sGrades.Grades = await _context.Grades.Where(g => g.StudentId == s.Id).Select(g => g.TestGrade).ToListAsync();
                sGrades.CategoryGrades = new Dictionary<int, List<int>>();
                var categoryGrades = await _context.CategoryGrades.Where(g => g.StudentId == s.Id).ToListAsync();
                Console.WriteLine("number of students: " + students.Count() + ", student Id = " + s.Id + ", number of grades " + sGrades.Grades.Count());
                if (categoryGrades != null)
                {
                    foreach (var catGrade in categoryGrades)
                    {
                        Console.WriteLine("categoryId = " + catGrade.CategoryId + ", grade = " + catGrade.Grade);
                        if (sGrades.CategoryGrades.ContainsKey(catGrade.CategoryId))
                        {
                            Console.WriteLine("ContainsKey, Adding grade to dict");
                            sGrades.CategoryGrades[catGrade.CategoryId].Add(catGrade.Grade);                            
                        }                            
                        else 
                        {
                            Console.WriteLine("Doesn't contain key, adding key with new List");
                            //List<int> grades = new List<int> {catGrade.Grade};
                            sGrades.CategoryGrades.Add(catGrade.CategoryId, new List<int> { catGrade.Grade });
                        }
                    }
                    studentsGrades.Add(sGrades);
                }

            }
            return studentsGrades;
        }


        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateStudent(Student student)
        {
            _context.Entry(student).State = EntityState.Modified;
        }

        public async Task<List<int>> GetStudentIdsByClassId(int classId)
        {
            return await _context.Students.Where(s => s.SchoolClassId == classId).Select(s => s.Id).ToListAsync();
        }


    }
}