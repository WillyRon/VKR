using Forest_fire_control.BI.ServiceInterfaces;
using Forest_fire_control.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace Forest_fire_control.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IObservationService _observationService;

        public UserController(IUserService userService, IObservationService observationService)
        {
            _userService = userService;
            _observationService = observationService;
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] UserModel user)
        {
            try
            {
                var region = await _observationService.GetOrCreateRegion(user.Region);
                var result = await _userService.CreateUser(user, region.Id);

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
                return StatusCode(500, new { ErrorMessage = $"Ошибка при создании пользователя {ex}" });
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
