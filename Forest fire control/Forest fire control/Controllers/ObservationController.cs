using Forest_fire_control.BI.ServiceInterfaces;
using Forest_fire_control.BI.Services;
using Forest_fire_control.Data.Entity;
using Forest_fire_control.Data.Model;
using Forest_fire_control.Data.Models;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("incedents")]
        public async Task<ActionResult<List<Incedent>>> GetIncedents()
        {
            var incedents = await _observationService.GetIncedents();
            return Ok(incedents);
        }

        [HttpPost("create-observation")]
        public async Task<IActionResult> CreateObservation([FromBody] ObservationSiteModel observation)
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

        [HttpPost("update-observation")]
        public async Task<IActionResult> UpdateObservation([FromBody] ObservationSiteModel observation)
        {
            var result = await _observationService.UpdateObservation(observation);

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { ErrorMessage = result.ErrorMessage });
            }
        }

        [HttpPost("delete-observation")]
        public async Task<IActionResult> DeleteObservation([FromBody] ObservationSiteModel observation)
        {
            var result = await _observationService.DeleteObservation(observation);

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { ErrorMessage = result.ErrorMessage });
            }
        }


        [HttpGet("get-all-observation")]
        public async Task<ActionResult<List<ObservationSiteModel>>> GetObservations()
        {
            var observations = await _observationService.GetObservations();
            return Ok(observations);
        }

        [HttpGet("get-incident-observation")]
        public async Task<ActionResult<List<Incedent>>> GetIncidentObservations(float longitude, float latitude)
        {
            var observation = await _observationService.GetObservation(longitude, latitude);
            var incidents = await _observationService.GetIncedentObservation(observation.Id);
            if(incidents != null)
            {
                return Ok(incidents);
            }

            return BadRequest();
        }

        [HttpGet("get-video-archive-observation")]
        public async Task<ActionResult<List<VideoArchive>>> GetVideoArchiveObservations(float longitude, float latitude)
        {
            var observation = await _observationService.GetObservation(longitude, latitude);
            var videoArchive = await _observationService.GetVideoArchiveObservation(observation.Id);
            if (videoArchive != null)
            {
                return Ok(videoArchive);
            }

            return BadRequest();
        }

        [HttpGet("archive-video/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var video = await _observationService.GetArchiveVideo(id);
            return video;
        }

    }
}
