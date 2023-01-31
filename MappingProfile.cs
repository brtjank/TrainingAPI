using System;
using AutoMapper;
using TrainingApp.Entities;
using TrainingApp.Models;

namespace TrainingApp
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Trainee, TraineeDto>();
			CreateMap<Session, SessionDto>();
			CreateMap<Exercise, ExerciseDto>();

            CreateMap<CreateTraineeDto, Trainee>();
            CreateMap<CreateSessionDto, Session>();
            CreateMap<CreateExerciseDto, Exercise>();
        }
    }
}

