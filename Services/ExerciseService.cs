using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.Entities;
using TrainingApp.Exceptions;
using TrainingApp.Models;

namespace TrainingApp.Services
{
    public interface IExerciseService
    {
        public IEnumerable<ExerciseDto> GetAll(int sessionId);
        public ExerciseDto GetById(int id);
        public int Create(CreateExerciseDto createExerciseDto);
        public void Delete(int id);
        public ExerciseDto Update(int id, UpdateExerciseDto updateExerciseDto);
        public int Multiply(int exerciseId, int sessionId);
    }

    public class ExerciseService : IExerciseService
    {
        private readonly TrainingDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ExerciseService> _logger;

        public ExerciseService(TrainingDbContext dbContext, IMapper mapper, ILogger<ExerciseService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        // get all of the exercises or just those from specific training session
        public IEnumerable<ExerciseDto> GetAll(int sessionId)
        {
            if (sessionId == 0)
            {
                var exercises = _dbContext
                    .Exercises
                    .ToList();
                return _mapper.Map<List<ExerciseDto>>(exercises);
            }
            else
            {
                var session = _dbContext
                    .Sessions
                    .FirstOrDefault(s => s.Id == sessionId);

                if (session is null)
                {
                    throw new NotFoundException($"Session with id {sessionId} not found");
                }

                var exercises = _dbContext
                    .Exercises
                    .Where(e => e.SessionId == sessionId)
                    .ToList();
                return _mapper.Map<List<ExerciseDto>>(exercises);
            }
        }

        public ExerciseDto GetById(int id)
        {
            var exercise = _dbContext.Exercises.Find(id);
            if (exercise is null)
            {
                throw new NotFoundException($"Exercise with id {id} not found");
            }
            return _mapper.Map<ExerciseDto>(exercise);
        }

        public int Create(CreateExerciseDto createExerciseDto)
        {
            var sessionId = createExerciseDto.SessionId;

            var session = _dbContext
                    .Sessions
                    .FirstOrDefault(s => s.Id == sessionId);

            if (session is null)
            {
                throw new NotFoundException($"Session with id {sessionId} not found");
            }

            var exercise = _mapper.Map<Exercise>(createExerciseDto);
            exercise.SessionId = sessionId;
            _dbContext.Exercises.Add(exercise);
            _dbContext.SaveChanges();

            return exercise.Id;
        }

        public void Delete(int id)
        {
            var exercise = _dbContext.Exercises.Find(id);
            if (exercise is not null)
            {
                _dbContext.Remove(exercise);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new NotFoundException($"Exercise with id {id} not found");
            }
        }

        public ExerciseDto Update(int id, UpdateExerciseDto updateExerciseDto)
        {
            var exercise = _dbContext.Exercises.Find(id);

            if (exercise is null)
            {
                throw new NotFoundException($"Exercise with id {id} not found");
            }

            exercise.Sets = updateExerciseDto.Sets;           
            exercise.Reps = updateExerciseDto.Reps;
            exercise.Load = updateExerciseDto.Load;

            _dbContext.SaveChanges();

            var exerciseDto = _mapper.Map<ExerciseDto>(exercise);

            return exerciseDto;
        }

        public int Multiply(int exerciseId, int sessionId)
        {
            var exercise = _dbContext
                .Exercises
                .FirstOrDefault(e => e.Id == exerciseId);

            if (exercise is null)
            {
                throw new NotFoundException($"Exercise with id {exerciseId} not found");
            }

            if (exercise.SessionId == sessionId)
            {
                throw new Exception($"Session with id {sessionId} already contains an exercise with id {exerciseId}");
            }

            var session = _dbContext
                .Sessions
                .FirstOrDefault(s => s.Id == sessionId);

            if (session is null)
            {
                throw new NotFoundException($"Session with id {sessionId} not found");
            }

            var exercise_new = new Exercise();
            exercise_new = exercise;

            exercise_new.Id = 0;
            exercise_new.Session = session;
            exercise_new.SessionId = sessionId;

            _dbContext.Exercises.Add(exercise_new);
            _dbContext.SaveChanges();

            return exercise_new.Id;
        }
    }
}

