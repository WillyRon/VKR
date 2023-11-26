using Forest_fire_control.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Forest_fire_control.Data.Model;
using Forest_fire_control.BI.ServiceInterfaces;
using System;
using Microsoft.EntityFrameworkCore;

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
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] UserModel user)
        {
            try
            {
                var result = await _userService.CreateUser(user);

                if (result.Success)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(new { ErrorMessage = result.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = "Ошибка при создании пользователя" });
            }
        }
        [HttpGet("user/{email}")]
        public async Task<IActionResult> GetUser(string email)
        {
            var user = await _userService.GetUser(email);

            if (user == null)
            {
                return NotFound($"User with email {email} not found.");
            }

            return Ok(user);
        }
    }
}