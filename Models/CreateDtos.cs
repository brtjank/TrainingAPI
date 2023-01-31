using System.ComponentModel.DataAnnotations;

namespace TrainingApp.Models
{
    public class CreateTraineeDto
    {
        public string Name { get; set; }
        public string Team { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }

    public class CreateSessionDto
    {
        public string Category { get; set; }
        public string Intensity { get; set; }
        public int Duration { get; set; }
        public DateTime Date { get; set; }
        public int TraineeId { get; set; }
    }

    public class CreateExerciseDto
    {
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public string Load { get; set; }
        public int SessionId { get; set; }
    }
}