using System;
using TrainingApp.Entities;

namespace TrainingApp.Models
{
	public class TraineeDto
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Team { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }

    public class SessionDto
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Intensity { get; set; }
        public int Duration { get; set; }
        public DateTime Date { get; set; }
        public int TraineeId { get; set; }
    }

    public class ExerciseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public string Load { get; set; }
        public int SessionId { get; set; }
    }
}

