using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Forest_fire_control.Data.Model;
using Forest_fire_control.BI.ServiceInterfaces;
using System;


namespace Forest_fire_control.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _userService.Login(model);

            if (result.Success)
            {
                return Ok(new { Token = result.Token });
            }
            else
            {
                return BadRequest(new { ErrorMessage = result.ErrorMessage });
            }
        }
    }
}