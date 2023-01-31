using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.Entities;
using TrainingApp.Exceptions;
using TrainingApp.Models;

namespace TrainingApp.Services
{
    public interface ITraineeService
    {
        public IEnumerable<TraineeDto> GetAll(string team = "none");
        public TraineeDto GetById(int id);
        public int Create(CreateTraineeDto createTraineeDto);
        public void Delete(int id);
        public TraineeDto Update(int id, UpdateTraineeDto updateTraineeDto);
    }

    public class TraineeService : ITraineeService
    {
        private readonly TrainingDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<TraineeService> _logger;

        public TraineeService(TrainingDbContext dbContext, IMapper mapper, ILogger<TraineeService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        // get all trainees or just those from specific team
        public IEnumerable<TraineeDto> GetAll(string team)
        {
            if(team == "none")
            {
                var trainees = _dbContext
                    .Trainees
                    .ToList();
                return _mapper.Map<List<TraineeDto>>(trainees);
            }
            else
            {
                var trainees = _dbContext
                    .Trainees
                    .Where(t => t.Team == team)
                    .ToList();
                return _mapper.Map<List<TraineeDto>>(trainees);
            }
        }

        public TraineeDto GetById(int id)
        {
            var trainee = _dbContext.Trainees.Find(id);
            if (trainee is null)
            {
                throw new NotFoundException($"Trainee with id {id} not found");
            }
            return _mapper.Map<TraineeDto>(trainee);
        }

        public int Create(CreateTraineeDto createTraineeDto)
        {
            var trainee = _mapper.Map<Trainee>(createTraineeDto);
            _dbContext.Trainees.Add(trainee);
            _dbContext.SaveChanges();

            return trainee.Id;
        }

        public void Delete(int id)
        {
            var trainee = _dbContext.Trainees.Find(id);
            if (trainee is not null)
            {
                _dbContext.Remove(trainee);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new NotFoundException($"Trainee with id {id} not found");
            }
        }

        public TraineeDto Update(int id, UpdateTraineeDto updateTraineeDto)
        {
            var trainee = _dbContext.Trainees.Find(id);

            if (trainee is null)
            {
                throw new NotFoundException($"Trainee with id {id} not found");
            }

            trainee.Team = updateTraineeDto.Team;           
            trainee.Email = updateTraineeDto.Email;
            trainee.PhoneNumber = updateTraineeDto.PhoneNumber;

            _dbContext.SaveChanges();

            var traineeDto = _mapper.Map<TraineeDto>(trainee);

            return traineeDto;
        }
    }
}

