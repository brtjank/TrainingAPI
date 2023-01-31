using System;
using System.Xml.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.Entities;
using TrainingApp.Models;
using TrainingApp.Services;

namespace TrainingApp.Controllers
{
	[Route("api/exercise")]
	[ApiController]
	public class ExerciseController : ControllerBase
	{
		private readonly IExerciseService _exerciseService;

		public ExerciseController(IExerciseService exerciseService)
		{
			_exerciseService = exerciseService;
		}

        // get all exercises, or those for specific training sessions
        [HttpGet]
		public ActionResult<IEnumerable<ExerciseDto>> GetAll([FromQuery(Name = "sessionId")] int sessionId = 0)
		{
			var exercisesDtos = _exerciseService.GetAll(sessionId);
			return Ok(exercisesDtos);
		}

		[HttpGet("{id}")]
		public ActionResult<ExerciseDto> Get(int id)
		{
			var exerciseDto = _exerciseService.GetById(id);
			return Ok(exerciseDto);
		}

        // Create new exercise instance assigned to a specific training session
        [HttpPost]
		public ActionResult Create([FromBody]CreateExerciseDto createExerciseDto)
		{
			int id = _exerciseService.Create(createExerciseDto);
			return Created($"api/exercise/{id}", null);
		}

		[HttpDelete("{id}")]
		public ActionResult Delete(int id)
		{
			_exerciseService.Delete(id);
			return NoContent();
		}

		[HttpPut("{id}")]
		public ActionResult Update([FromRoute] int id, [FromBody] UpdateExerciseDto updateExerciseDto)
		{
            var exerciseDto = _exerciseService.Update(id, updateExerciseDto);
            return Ok(exerciseDto);

        }

        // Copy the exercise with given id, and assign it to a training session with given id
        [HttpPost("{exerciseId}/{sessionId}")]
        public ActionResult Multiply(int exerciseId, int sessionId)
        {
            int id = _exerciseService.Multiply(exerciseId, sessionId);
            return Created($"api/exercise/{id}", null);
        }
    }
}
