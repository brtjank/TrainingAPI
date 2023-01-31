using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.Entities;
using TrainingApp.Models;
using TrainingApp.Services;

namespace TrainingApp.Controllers
{
	[Route("api/session")]
	[ApiController]
	public class SessionController : ControllerBase
	{
		private readonly ISessionService _sessionService;

		public SessionController(ISessionService sessionService)
		{
			_sessionService = sessionService;
		}

        // get all training sessions, or those for specific trainee or specific date
        [HttpGet]
		public ActionResult<IEnumerable<SessionDto>> GetAll([FromQuery(Name = "traineeId")] int traineeId = 0, [FromQuery(Name = "date")] string date = "")
		{
			if (traineeId == 0 & date == "")
			{
                var sessionsDtos = _sessionService.GetAll();
                return Ok(sessionsDtos);
            }
            else if (traineeId != 0 & date == "")
            {
                var sessionsDtos = _sessionService.GetAllFromTrainee(traineeId);
                return Ok(sessionsDtos);
            }
            else if (traineeId == 0 & date != "")
            {
                var sessionsDtos = _sessionService.GetAllFromDate(date);
                return Ok(sessionsDtos);
            }
			else
            {
                var sessionsDtos = _sessionService.GetAllFromTraineeFromDate(traineeId, date);
                return Ok(sessionsDtos);
            }
        }

		[HttpGet("{id}")]
		public ActionResult<SessionDto> Get(int id)
		{
			var sessionDto = _sessionService.GetById(id);
			return Ok(sessionDto);
		}

        // Create new training session instance assigned to a specific trainee
        [HttpPost]
		public ActionResult Create([FromBody]CreateSessionDto createSessionDto)
		{
			int id = _sessionService.Create(createSessionDto);
			return Created($"api/session/{id}", null);
		}

		[HttpDelete("{id}")]
		public ActionResult Delete(int id)
		{
			_sessionService.Delete(id);
			return NoContent();
		}

		[HttpPut("{id}")]
		public ActionResult Update([FromRoute] int id, [FromBody] UpdateSessionDto updateSessionDto)
		{
            var sessionDto = _sessionService.Update(id, updateSessionDto);
            return Ok(sessionDto);

        }

		// Copy the session with given id, and assign it to trainee with given id
		[HttpPost("{sessionId}/{traineeId}")]
		public ActionResult Multiply(int sessionId,int traineeId)
		{
            int id = _sessionService.Multiply(sessionId, traineeId);
            return Created($"api/session/{id}", null);
        }
	}
}
