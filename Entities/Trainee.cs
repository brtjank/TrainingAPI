using System;

namespace TrainingApp.Entities
{
    public class Trainee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Team { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public virtual List<Session> Sessions { get; set; }
    }
}

