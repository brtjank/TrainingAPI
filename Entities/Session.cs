using System;

namespace TrainingApp.Entities
{
	public class Session
	{
        public int Id { get; set; }
        public string Category { get; set; }
        public string Intensity { get; set; }
        public int Duration { get; set; }
        public DateTime Date { get; set; }
        public int TraineeId { get; set; }
        public virtual Trainee Trainee { get; set; }
        public virtual List<Exercise> Exercises { get; set; }
    }
}

