using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PB_EGS_Escalavel.Application.InputModels;
using PB_EGS_Escalavel.Application.Services.Interfaces;
using PB_EGS_Escalavel.Core.Entities;

namespace PB_EGS_Escalavel.API.Controllers
{
    [Route("api/courses")]
    [Authorize]
    [ApiController]
    public class CoursesController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICourseService _courseService;
        public CoursesController(IUserService userService, ICourseService courseService)
        {
            _userService = userService;
            _courseService = courseService;
        }

        // api/courses
        [HttpGet]
        [Authorize(Roles = "teacher, student")]
        public async Task<IActionResult> Get()
        {
            var courses = await _courseService.GetAllAsync();

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
        [HttpPut("{teacherId}")]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> Put(int teacherId, [FromBody] UpdateCourseInputModel inputModel)
        {
            if (inputModel.Description.Length > 200)
            {
                return BadRequest();
            }

            if (!await _courseService.UpdateAsync(inputModel, teacherId)) return NotFound();

            return CreatedAtAction(nameof(GetById), new { id = inputModel.Id }, inputModel);
        }

        // api/courses/3/1 DELETE
        [HttpDelete("{id}/{teacherId}")]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> Delete(int id, int teacherId)
        {
            if(! await _courseService.DeleteAsync(id, teacherId)) return NotFound();

            return Ok();
        }

        [HttpPost("students")]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> AddStudentToCourse([FromBody] NewStudentCourseInputModel inputModel)
        {
            if (!await _courseService.AddStudentCourseAsync(inputModel)) return NotFound();

            return Ok();
        }
    }
}
