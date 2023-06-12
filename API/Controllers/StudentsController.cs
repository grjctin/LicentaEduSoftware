using System.ComponentModel.DataAnnotations;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class StudentsController : BaseApiController
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        public StudentsController(IStudentRepository studentRepository, IMapper mapper)
        {
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

        [HttpGet("details/classId={classId}")]
        public async Task<ActionResult<List<Student>>> GetStudentsDetailsByClassId(int classId)
        {
            return await _studentRepository.GetStudentsDetailsByClassId(classId);
        }

        [HttpGet("grades/classId={classId}")]
        public async Task<ActionResult<List<StudentsGradesDto>>> GetStudentsGradesByClassId(int classId)
        {
            Console.WriteLine("ASFDGFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF");
            return await _studentRepository.GetStudentsGradesByClassId(classId);
        }

        [HttpGet("")]

        [HttpGet("get-student-by-name")]
        public async Task<ActionResult<Student>> GetStudentByName([FromQuery] string firstname, [FromQuery] string lastname)
        {
            Console.WriteLine("firstname = " + firstname + ", lastname = " + lastname);
            return await _studentRepository.GetStudentByName(firstname, lastname);
        }

        [HttpPost]
        public async Task<ActionResult> AddStudent(StudentDto studentDto)
        {
            // var student = new Student
            // {
            //     FirstName = studentDto.FirstName,
            //     LastName = studentDto.LastName,
            //     SchoolClassId = studentDto.SchoolClassId,
            //     Cnp = studentDto.Cnp,
            //     Email = studentDto.Email,
            //     PhoneNumber = studentDto.PhoneNumber
            // };
            var student = new Student();

            _mapper.Map(studentDto,student);

            _studentRepository.AddStudent(student);

            if (await _studentRepository.SaveAllAsync())
                return Ok("Student added successfully");
            return BadRequest("Failed to add student");

        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var student = await _studentRepository.GetStudentById(id);

            if(student == null)
                return BadRequest("User does not exist");

            _studentRepository.DeleteStudent(student);

            if(await _studentRepository.SaveAllAsync())
                return Ok("Student deleted");
            return BadRequest("Problem deleting student");
        }
    }
}