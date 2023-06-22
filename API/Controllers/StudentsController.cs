using System.ComponentModel.DataAnnotations;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore.Internal;

namespace API.Controllers
{
    public class StudentsController : BaseApiController
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly IAttendanceRepository _attendanceRepository;
        public StudentsController(IStudentRepository studentRepository, IMapper mapper, IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
            _mapper = mapper;
            _studentRepository = studentRepository;

        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            return await _studentRepository.GetStudents();
        }

        [HttpGet("studentId={id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            return await _studentRepository.GetStudentById(id);
        }

        // [HttpGet("details/classId={classId}")]
        // public async Task<ActionResult<List<Student>>> GetStudentsDetailsByClassId(int classId)
        // {
        //     return await _studentRepository.GetStudentsDetailsByClassId(classId);
        // }

        [HttpGet("details/classId={classId}")]
        public async Task<ActionResult<List<StudentDto>>> GetStudentsDetailsByClassId(int classId)
        {
            var students = await _studentRepository.GetStudentsDetailsByClassId(classId);
            var studentDtos = _mapper.Map<List<StudentDto>>(students);

            return Ok(studentDtos);
        }

        [HttpGet("grades/classId={classId}")]
        public async Task<ActionResult<List<StudentsGradesDto>>> GetStudentsGradesByClassId(int classId)
        {
            await Task.Delay(1000);
            var studentsGrades = await _studentRepository.GetStudentsGradesByClassId(classId);
            return Ok(studentsGrades);
        }

        [HttpGet("attendance/classId={classId}")]
        public async Task<ActionResult<List<StudentsAttendanceDto>>> GetStudentsAttendanceByClassId(int classId)
        {
            await Task.Delay(1000);
            var students = await _studentRepository.GetStudentsDetailsByClassId(classId);
            List<StudentsAttendanceDto> response = new List<StudentsAttendanceDto>();

            foreach(var s in students)
            {
                var attendance = await _attendanceRepository.GetStudentAttendance(s.Id);
                response.Add(new StudentsAttendanceDto{
                    Id=s.Id,
                    FirstName=s.FirstName,
                    LastName = s.LastName,
                    PresenceDates = attendance
                });
            }
            
            return Ok(response);
        }

        [HttpGet("get-student-by-name")]
        public async Task<ActionResult<Student>> GetStudentByName([FromQuery] string firstname, [FromQuery] string lastname)
        {
            return await _studentRepository.GetStudentByName(firstname, lastname);
        }

        [HttpPost]
        public async Task<ActionResult> AddStudent(StudentDto studentDto)
        {
            var student = new Student();

            _mapper.Map(studentDto, student);

            _studentRepository.AddStudent(student);

            if (await _studentRepository.SaveAllAsync())
                return Ok("Student added successfully");
            return BadRequest("Failed to add student");
        }

        [HttpPost("add-attendance")]
        public async Task<ActionResult> AddAttendance(List<int> studentIds)
        {
            _attendanceRepository.AddStudentsAttendance(studentIds);
            if(await _attendanceRepository.SaveAllAsync())
                return Ok("Attendance added");
            return BadRequest("Failed to add attendance");
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var student = await _studentRepository.GetStudentById(id);

            if (student == null)
                return BadRequest("User does not exist");

            _studentRepository.DeleteStudent(student);

            if (await _studentRepository.SaveAllAsync())
                return Ok("Student deleted");
            return BadRequest("Problem deleting student");
        }
    }
}