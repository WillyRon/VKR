using Forest_fire_control.BI.ServiceInterfaces;
using Forest_fire_control.BI.Services;
using Forest_fire_control.Data.Entity;
using Forest_fire_control.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forest_fire_control.Controllers
{
    [Route("api/observation")]
    [ApiController]
    public class ObservationController : ControllerBase
    {
        private readonly IObservationService _observationService;

        public ObservationController(IObservationService observationService)
        {
            _observationService = observationService;
        }

        [HttpGet("regions")]
        public async Task<ActionResult<List<Region>>> GetRegions()
        {
            var regions = await _observationService.GetRegions();
            return Ok(regions);
        }

        [HttpPost("create-observation")]
        public async Task<IActionResult> CreateUser([FromBody] ObservationSiteModel observation)
        {
            try
            {
                var result = await _observationService.CreateObservation(observation);

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
                return StatusCode(500, new { ErrorMessage = $"Ошибка при создании точки {ex}" });
            }
        }

        [HttpGet("get-all-observation")]
        public async Task<ActionResult<List<ObservationSiteModel>>> GetObservations()
        {
            var observations = await _observationService.GetObservations();
            return Ok(observations);
        }

    }
}
