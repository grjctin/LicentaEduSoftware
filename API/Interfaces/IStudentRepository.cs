using API.DTOs;
using API.Entities;
using Microsoft.EntityFrameworkCore.Internal;

namespace API.Interfaces
{
    public interface IStudentRepository
    {
         Task<List<Student>> GetStudents();
         Task<List<Student>> GetStudentsDetailsByClassId(int id);
         Task<List<StudentsGradesDto>> GetStudentsGradesByClassId(int id);
         Task<Student> GetStudentByName(string firstname, string lastname);
         Task<Student> GetStudentById(int id);
         void UpdateStudent(Student student);
         void AddStudent(Student student);
         Task<bool> SaveAllAsync();
         void DeleteStudent(Student student);

         Task<string> GetStudentNameById(int studentId);

         Task<List<int>> GetStudentIdsByClassId(int classId);

    }
}