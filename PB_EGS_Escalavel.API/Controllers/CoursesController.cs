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
        [Authorize(Roles = "teacher, student")]
        public async Task<IActionResult> Get(string? query)
        {
            var courses = await _courseService.GetAllAsync(query);

            return Ok(courses);
        }

        // api/courses/2
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var course = await _courseService.GetByIdAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        [HttpPost]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> Post([FromBody] NewCourseInputModel inputModel)
        {
            if (inputModel.Title.Length > 50)
            {
                return BadRequest();
            }

            var id = await _courseService.CreateAsync(inputModel);

            return CreatedAtAction(nameof(GetById), new { id = id }, inputModel);
        }

        // api/courses/2
        [HttpPut("{id}")]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCourseInputModel inputModel)
        {
            if (inputModel.Description.Length > 200)
            {
                return BadRequest();
            }

            await _courseService.UpdateAsync(inputModel);

            return CreatedAtAction(nameof(GetById), new { id = id }, inputModel);
        }

        // api/courses/3 DELETE
        [HttpDelete("{id}")]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseService.DeleteAsync(id);

            return Ok();
        }

        [HttpPost("students")]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> AddStudentToCourse([FromBody] NewStudentCourseInputModel inputModel)
        {
            await _courseService.AddStudentCourseAsync(inputModel);

            return Ok();
        }
    }
}
