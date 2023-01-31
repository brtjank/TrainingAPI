using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingApp.Entities;
using TrainingApp.Models;
using TrainingApp.Services;

namespace TrainingApp.Controllers
{
	[Route("api/trainee")]
	[ApiController]
	public class TraineeController : ControllerBase
	{
		private readonly ITraineeService _traineeService;

		public TraineeController(ITraineeService traineeService)
		{
			_traineeService = traineeService;
		}

		// gets all trainees or just those from specific team
		[HttpGet]
		public ActionResult<IEnumerable<TraineeDto>> GetAll([FromQuery] string name = "none")
		{
			var traineesDtos = _traineeService.GetAll(name);
			return Ok(traineesDtos);
		}

		[HttpGet("{id}")]
		public ActionResult<TraineeDto> Get(int id)
		{
			var traineeDto = _traineeService.GetById(id);
			return Ok(traineeDto);
		}

		[HttpPost]
		public ActionResult Create([FromBody]CreateTraineeDto createTraineeDto)
		{
			int id = _traineeService.Create(createTraineeDto);
			return Created($"api/trainee/{id}", null);
		}

		[HttpDelete("{id}")]
		public ActionResult Delete(int id)
		{
			_traineeService.Delete(id);
			return NoContent();
		}

		[HttpPut("{id}")]
		public ActionResult Update([FromRoute] int id, [FromBody] UpdateTraineeDto updateTraineeDto)
		{
            var traineeDto = _traineeService.Update(id, updateTraineeDto);
            return Ok(traineeDto);

        }
	}
}
