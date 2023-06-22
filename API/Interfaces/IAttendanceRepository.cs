using API.Entities;

namespace API.Interfaces
{
    public interface IAttendanceRepository
    {
         void AddStudentsAttendance(List<int> studentIds);

         Task<List<DateTime>> GetStudentAttendance(int studentId);

         Task<bool> SaveAllAsync();
    }
}