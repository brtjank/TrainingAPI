using System;
using System.ComponentModel.DataAnnotations;

namespace TrainingApp.Models
{
    public class UpdateTraineeDto
    {
        public string Team { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }

    public class UpdateSessionDto
    {
        public string Intensity { get; set; }
        public int Duration { get; set; }
        public DateTime Date { get; set; }
    }

    public class UpdateExerciseDto
	{
        public int Sets { get; set; }
        public int Reps { get; set; }
        public string Load { get; set; }
    }
}

