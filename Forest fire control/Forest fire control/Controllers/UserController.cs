using Forest_fire_control.BI.ServiceInterfaces;
using Forest_fire_control.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Forest_fire_control.Data.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using ApplicationModel = Forest_fire_control.Data.Model.ApplicationModel;

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

        [HttpPost("create-application")]
        public async Task<IActionResult> CreateApplication([FromBody] ApplicationModel applicationModel)
        {
            try
            {
                var observation = applicationModel.ObservationSite != null
                  ? await _observationService.GetObservation(applicationModel.ObservationSite.Longitude, applicationModel.ObservationSite.Latitude)
                  : null;

                var observationId = observation?.Id;

                var result = await _userService.CreateApplication(applicationModel, observationId);

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
                return StatusCode(500, new { ErrorMessage = $"Ошибка при создании заявки {ex}" });
            }
        }

        [HttpGet("applications")]
        public async Task<ActionResult<List<ApplicationModel>>> GetIncedents()
        {
            var applications = await _userService.GetApplications();
            return Ok(applications);
        }

        [HttpPost("change-application-status")]
        public async Task<IActionResult> ChangeApplicationStatus([FromBody] ApplicationModel applicationModel)
        {
            var result = await _userService.ChangeApplicationStatus(applicationModel);
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { ErrorMessage = result.ErrorMessage });
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
