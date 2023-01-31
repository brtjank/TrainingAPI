using System;
using System.Globalization;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.Entities;
using TrainingApp.Exceptions;
using TrainingApp.Models;

namespace TrainingApp.Services
{
    public interface ISessionService
    {
        public IEnumerable<SessionDto> GetAll();
        public IEnumerable<SessionDto> GetAllFromTrainee(int traineeId);
        public IEnumerable<SessionDto> GetAllFromDate(string dateString);
        public IEnumerable<SessionDto> GetAllFromTraineeFromDate(int traineeId, string dateString);
        public SessionDto GetById(int id);
        public int Create(CreateSessionDto createSessionDto);
        public void Delete(int id);
        public SessionDto Update(int id, UpdateSessionDto updateSessionDto);
        public int Multiply(int sessionId, int traineeId);
    }

    public class SessionService : ISessionService
    {
        private readonly TrainingDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<SessionService> _logger;

        public SessionService(TrainingDbContext dbContext, IMapper mapper, ILogger<SessionService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        // get all training sessions
        public IEnumerable<SessionDto> GetAll()
        {
            var sessions = _dbContext
                .Sessions
                .ToList();
            return _mapper.Map<List<SessionDto>>(sessions);
        }

        // get all of the training sessions planned for the trainee with given id
        public IEnumerable<SessionDto> GetAllFromTrainee(int traineeId)
        {
            var trainee = _dbContext
                .Trainees
                .FirstOrDefault(t => t.Id == traineeId);

            if (trainee is null)
            {
                throw new NotFoundException($"Trainee with id {traineeId} not found");
            }

            var sessions = _dbContext
                .Sessions
                .Where(s => s.TraineeId == traineeId)
                .ToList();
            return _mapper.Map<List<SessionDto>>(sessions);
        }


        // get all of the training sessions planned for the given day
        public IEnumerable<SessionDto> GetAllFromDate(string dateString)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime date = DateTime.ParseExact(dateString, "d", provider);

            var sessions = _dbContext
                .Sessions
                .Where(s => s.Date == date)
                .ToList();
            return _mapper.Map<List<SessionDto>>(sessions);
        }


        // get all of the training sessions planned for the trainee with given id for the given day
        public IEnumerable<SessionDto> GetAllFromTraineeFromDate(int traineeId, string dateString)
        {
            var trainee = _dbContext
                .Trainees
                .FirstOrDefault(t => t.Id == traineeId);

            if (trainee is null)
            {
                throw new NotFoundException($"Trainee with id {traineeId} not found");
            }

            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime date = DateTime.ParseExact(dateString, "d", provider);

            var sessions = _dbContext
                .Sessions
                .Where(s => s.TraineeId == traineeId)
                .Where(s => s.Date == date)
                .ToList();
            return _mapper.Map<List<SessionDto>>(sessions);
        }

        public SessionDto GetById(int id)
        {
            var session = _dbContext.Sessions.Find(id);
            if (session is null)
            {
                throw new NotFoundException($"Session with id {id} not found");
            }
            return _mapper.Map<SessionDto>(session);
        }

        // Create new training session instance assigned to a specific trainee
        public int Create(CreateSessionDto createSessionDto)
        {
            var traineeId = createSessionDto.TraineeId;

            var trainee = _dbContext
                    .Trainees
                    .FirstOrDefault(t => t.Id == traineeId);

            if (trainee is null)
            {
                throw new NotFoundException($"Trainee with id {traineeId} not found");
            }

            var session = _mapper.Map<Session>(createSessionDto);
            session.TraineeId = traineeId;
            _dbContext.Sessions.Add(session);
            _dbContext.SaveChanges();

            return session.Id;
        }

        public void Delete(int id)
        {
            var session = _dbContext.Sessions.Find(id);
            if (session is not null)
            {
                _dbContext.Remove(session);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new NotFoundException($"Session with id {id} not found");
            }
        }

        public SessionDto Update(int id, UpdateSessionDto updateSessionDto)
        {
            var session = _dbContext.Sessions.Find(id);

            if (session is null)
            {
                throw new NotFoundException($"Session with id {id} not found");
            }

            session.Intensity = updateSessionDto.Intensity;
            session.Duration = updateSessionDto.Duration;
            session.Date = updateSessionDto.Date;

            _dbContext.SaveChanges();

            var sessionDto = _mapper.Map<SessionDto>(session);

            return sessionDto;
        }

        // create a copy of a training session along with all of its exercises
        // and assign it to another trainee
        public int Multiply(int sessionId, int traineeId)
        {
            var session = _dbContext
                .Sessions
                .FirstOrDefault(s => s.Id == sessionId);

            if (session is null)
            {
                throw new NotFoundException($"Session with id {sessionId} not found");
            }

            if (session.TraineeId == traineeId)
            {
                throw new Exception($"Trainee with id {traineeId} is already linked " +
                    $"with session with id {sessionId}");
            }

            var trainee = _dbContext
                .Trainees
                .FirstOrDefault(t => t.Id == traineeId);

            if (trainee is null)
            {
                throw new NotFoundException($"Trainee with id {traineeId} not found");
            }

            var session_new = new Session();
            session_new = session;

            session_new.Id = 0;
            session_new.Trainee = trainee;
            session_new.TraineeId = traineeId;

            _dbContext.Sessions.Add(session_new);
            _dbContext.SaveChanges();

            if (session_new is null)
            {
                throw new Exception($"Something went wrong. " +
                    $"Cannot multiply session with id {sessionId}. " +
                    $"Therefore, cannot multiply its exercises");
            }

            var exercises = _dbContext
                .Exercises
                .Where(e => e.SessionId == sessionId)
                .ToList();

            foreach (Exercise e in exercises)
            {
                var exercise_new = new Exercise();
                exercise_new = e;

                exercise_new.Id = 0;
                exercise_new.Session = session_new;
                exercise_new.SessionId = session_new.Id;

                _dbContext.Exercises.Add(e);
            }

            _dbContext.SaveChanges();

            return session_new.Id;
        }
    }
}

