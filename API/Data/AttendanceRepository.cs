using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly DataContext _context;
        public AttendanceRepository(DataContext context)
        {
            _context = context;

        }
        public void AddStudentsAttendance(List<int> studentIds)
        {
            foreach(int id in studentIds) {
                Attendance a = new Attendance{
                    StudentId = id,
                    PresenceDate = DateTime.Now
                };
                _context.Attendance.Add(a);
            }
        }

        public async Task<List<DateTime>> GetStudentAttendance(int studentId)
        {
            List<DateTime> dates = new List<DateTime>();
            dates.AddRange(await _context.Attendance.Where(a => a.StudentId == studentId).Select(a => a.PresenceDate).ToListAsync());
            return dates;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}