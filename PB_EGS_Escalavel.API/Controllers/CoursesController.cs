using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PB_EGS_Escalavel.Application.InputModels;
using PB_EGS_Escalavel.Application.Services.Interfaces;

namespace PB_EGS_Escalavel.API.Controllers
{
    [Route("api/courses")]
    [Authorize]
    [ApiController]
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // api/courses?query=net core
        [HttpGet]
        public IActionResult Get(string query)
        {
            var courses = _courseService.GetAll(query);

            return Ok(courses);
        }

        // api/courses/2
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var course = _courseService.GetById(id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        [HttpPost]
        public IActionResult Post([FromBody] NewCourseInputModel inputModel)
        {
            if (inputModel.Title.Length > 50)
            {
                return BadRequest();
            }

            var id = _courseService.Create(inputModel);

            return CreatedAtAction(nameof(GetById), new { id = id }, inputModel);
        }

        // api/courses/2
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateCourseInputModel inputModel)
        {
            if (inputModel.Description.Length > 200)
            {
                return BadRequest();
            }

            _courseService.Update(inputModel);

            return NoContent();
        }

        // api/courses/3 DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _courseService.Delete(id);

            return NoContent();
        }

        // api/courses/1/students POST
        [HttpPost("{id}/students")]
        public IActionResult AddStudentToCourse(int id, [FromBody] NewStudentCourseInputModel inputModel)
        {
            _courseService.AddStudentCourse(inputModel);

            return NoContent();
        }
    }
}
