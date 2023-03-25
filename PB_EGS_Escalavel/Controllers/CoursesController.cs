using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PB_EGS_Escalavel.Application.Services.Interfaces;

namespace PB_EGS_Escalavel.Controllers
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

        // api/projects?query=net core
        [HttpGet]
        public IActionResult Get(string query)
        {
            var projects = _projectService.GetAll(query);

            return Ok(projects);
        }
    }
}
