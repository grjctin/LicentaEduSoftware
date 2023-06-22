using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class GradesController : BaseApiController
    {
        private readonly IGradeRepository _gradeRepository;
        public GradesController(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;

        }

        [HttpPost("categoryGrades")]
        public async Task<ActionResult> AddCategoryGrade(CategoryGrade categoryGrade)
        {
            // + Validari, daca exista studentul si categoria
            if (categoryGrade.Grade > 0 && categoryGrade.Grade <= 10)
                _gradeRepository.AddCategoryGrade(categoryGrade);
            else return BadRequest("Not a valid grade!");

            if (await _gradeRepository.SaveAllAsync())
                return Ok("Grade added successfully response from controller");
            return BadRequest("Failed to add grade");
        }

        [HttpPost]
        public async Task<ActionResult> AddGrade(GradeParams gradeParams)
        {
            if (gradeParams.Grade > 0 && gradeParams.Grade <= 10)
            {
                var fullGrade = new Grade
                {
                    StudentId = gradeParams.StudentId,
                    TestGrade = gradeParams.Grade,
                    DateReceived = DateTime.Now
                };
                _gradeRepository.AddGrade(fullGrade);
            }
            else return BadRequest("Not a valid grade!");

            if (await _gradeRepository.SaveAllAsync())
                return Ok("Grade added succesfully");
            return BadRequest("Failed to add grade");
        }
    }
}