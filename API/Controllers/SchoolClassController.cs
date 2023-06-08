using API.Data;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class SchoolClassController : BaseApiController
    {
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly IUserRepository _userRepository;
        public SchoolClassController(ISchoolClassRepository schoolClassRepository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _schoolClassRepository = schoolClassRepository;

        }

        [HttpGet]
        public async Task<ActionResult<List<SchoolClass>>> GetClasses()
        {
            return await _schoolClassRepository.GetSchoolClasses();
        }

        [HttpGet("professorId")]
        public async Task<ActionResult<List<SchoolClass>>> GetClassesByProfessorId()
        {
            //Id-ul profesorului logat pentru a isi putea vedea clasele sale
            var professorId = User.GetUserId();
            return await _schoolClassRepository.GetSchoolClassesByProfessorId(professorId);
        }

        [HttpGet("classNumber={classNumber}")]
        public async Task<ActionResult<List<SchoolClass>>> GetClassesByClassNumber(int classNumber)
        {
            return await _schoolClassRepository.GetSchoolClassesByClassNumber(classNumber);
        }

        [HttpPost]
        public async Task<ActionResult> AddClass(SchoolClass schoolClass)
        {
            //verific daca mai exista clasa
            //verific daca exista profesorul
            if (await _schoolClassRepository.ClassNameExists(schoolClass.ClassName))
                return BadRequest("Class already exists");

            var professor = await _userRepository.GetUserByIdAsync(schoolClass.ProfessorId);
            if(professor == null)
                return BadRequest("Professor does not exist, name another professor");

            _schoolClassRepository.AddSchoolClass(schoolClass);
            if(await _schoolClassRepository.SaveAllAsync())
                return Ok("Class added");

            return BadRequest("Failed to add class");
        }
    }
}