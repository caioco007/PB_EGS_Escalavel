using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IAccountService _accountService;
        public UsersController(IUserService userService, IAccountService accountService)
        {
            _userService = userService;
            _accountService = accountService;
        }

        // api/users/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetUserAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // api/users
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] NewUserInputModel inputModel)
        {
            var id = await _userService.CreateAsync(inputModel);

            return CreatedAtAction(nameof(GetById), new { id = id }, inputModel);
        }

        // api/users/1/login
        [HttpPut("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserInputModel inputModel)
        {
            var loginUserViewModel = await _accountService.Login(inputModel);

            if (loginUserViewModel == null) return BadRequest();

            return Ok(loginUserViewModel);
        }
    }
}
