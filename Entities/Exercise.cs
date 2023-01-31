using System;

namespace TrainingApp.Entities
{
	public class Exercise
	{
		public int Id { get; set; }
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public string Load { get; set; }
        public int SessionId { get; set; }
        public virtual Session Session { get; set; }
    }
}

