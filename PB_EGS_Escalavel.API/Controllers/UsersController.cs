using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PB_EGS_Escalavel.API.Models;
using PB_EGS_Escalavel.Application.InputModels;
using PB_EGS_Escalavel.Application.Services.Interfaces;

namespace PB_EGS_Escalavel.API.Controllers
{
    [Route("api/users")]
    [Authorize]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // api/users/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // api/users
        [HttpPost]
        public IActionResult Post([FromBody] NewUserInputModel inputModel)
        {
            var id = _userService.Create(inputModel);

            return CreatedAtAction(nameof(GetById), new { id = id }, inputModel);
        }

        // api/users/1/login
        [HttpPut("{id}/login")]
        public IActionResult Login(int id, [FromBody] LoginModel login)
        {
            // TODO: Para Módulo de Autenticação e Autorização

            return NoContent();
        }
    }
}
