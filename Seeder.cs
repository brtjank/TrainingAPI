using System;
using TrainingApp.Entities;

namespace TrainingApp
{
	public class Seeder
	{
		private readonly TrainingDbContext _dbContext;

		public Seeder(TrainingDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void Seed()
		{
			if(_dbContext.Database.CanConnect())
			{
				if(!_dbContext.Trainees.Any())
				{
					var trainees = GetTrainees();
					_dbContext.Trainees.AddRange(trainees);
					_dbContext.SaveChanges();
				}
			}
			else
			{
				throw new Exception("Cannot connect to database");
			}
		}

        private IEnumerable<Trainee> GetTrainees()
        {
			var trainees = new List<Trainee>()
			{
				new Trainee()
				{
					Name = "Janusz Nowak",
					Team = "Wisla Krakow",
					PhoneNumber = "+48930291492",
					Email = "jannow@gmail.com",
					Sessions = new List<Session>()
					{
						new Session()
						{
							Category = "Upper Body",
							Intensity = "Mid",
							Duration = 90,
							Date = new DateTime(2023, 1, 25),
							Exercises = new List<Exercise>()
							{
								new Exercise()
								{
									Name = "Bench Press",
									Sets = 3,
									Reps = 8,
									Load = "80% Max"
								},
                                new Exercise()
                                {
                                    Name = "Military Press",
                                    Sets = 3,
                                    Reps = 10,
                                    Load = "70% Max"
                                }
                            }
						}
					}
				},
                new Trainee()
                {
                    Name = "Michal Kowalski",
                    Team = "Legia Warszawa",
                    PhoneNumber = "+48837213492",
                    Email = "michkow@gmail.com",
                    Sessions = new List<Session>()
                    {
                        new Session()
                        {
                            Category = "Conditioning",
                            Intensity = "High",
                            Duration = 40,
                            Date = new DateTime(2023, 1, 25),
                            Exercises = new List<Exercise>()
                            {
                                new Exercise()
                                {
                                    Name = "Warm up jogging 20 minutes",
                                    Sets = 1,
                                    Reps = 1,
                                    Load = "40% Max"
                                },
                                new Exercise()
                                {
                                    Name = "Interval Sprints 20s/40s",
                                    Sets = 3,
                                    Reps = 6,
                                    Load = "100% Max"
                                }
                            }
                        },
                        new Session()
                        {
                            Category = "Lower Body",
                            Intensity = "Low",
                            Duration = 60,
                            Date = new DateTime(2023, 1, 26),
                            Exercises = new List<Exercise>()
                            {
                                new Exercise()
                                {
                                    Name = "Back Squat",
                                    Sets = 4,
                                    Reps = 8,
                                    Load = "60% Max"
                                },
                                new Exercise()
                                {
                                    Name = "Deadlift",
                                    Sets = 3,
                                    Reps = 8,
                                    Load = "50% Max"
                                },
                                new Exercise()
                                {
                                    Name = "Harmstring curls",
                                    Sets = 3,
                                    Reps = 6,
                                    Load = "40% Max"
                                }
                            }
                        }
                    }
                },
                new Trainee()
                {
                    Name = "Pawel Wrzesien",
                    Team = "Wisla Krakow",
                    PhoneNumber = "+48743928472",
                    Email = "pawelw@gmail.com",
                    Sessions = new List<Session>()
                    {
                        new Session()
                        {
                            Category = "Full Body Workout",
                            Intensity = "High",
                            Duration = 60,
                            Date = new DateTime(2023, 2, 4),
                            Exercises = new List<Exercise>()
                            {
                                new Exercise()
                                {
                                    Name = "Kettlebell swings",
                                    Sets = 3,
                                    Reps = 12,
                                    Load = "50% Max"
                                },
                                new Exercise()
                                {
                                    Name = "Single arm dumbell row",
                                    Sets = 3,
                                    Reps = 10,
                                    Load = "70% Max"
                                },
                                new Exercise()
                                {
                                    Name = "Front squat",
                                    Sets = 3,
                                    Reps = 10,
                                    Load = "60% Max"
                                }
                            }
                        },
                        new Session()
                        {
                            Category = "Upper Body",
                            Intensity = "Mid",
                            Duration = 75,
                            Date = new DateTime(2023, 2, 2),
                            Exercises = new List<Exercise>()
                            {
                                new Exercise()
                                {
                                    Name = "Bench Press",
                                    Sets = 4,
                                    Reps = 8,
                                    Load = "60% Max"
                                },
                                new Exercise()
                                {
                                    Name = "Bicep curls",
                                    Sets = 3,
                                    Reps = 8,
                                    Load = "70% Max"
                                },
                                new Exercise()
                                {
                                    Name = "Tricep Extensions",
                                    Sets = 3,
                                    Reps = 8,
                                    Load = "60% Max"
                                }
                            }
                        }
                    }
                }
            };

			return trainees;
        }
    }
}

