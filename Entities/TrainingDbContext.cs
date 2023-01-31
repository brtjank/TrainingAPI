using System;
using Microsoft.EntityFrameworkCore;

namespace TrainingApp.Entities
{
	public class TrainingDbContext : DbContext
	{
        protected readonly IConfiguration Configuration;

        public TrainingDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Exercise> Exercises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trainee>()
                .Property(t => t.Name)
                .IsRequired();

            modelBuilder.Entity<Exercise>()
                .Property(e => e.Name)
                .IsRequired();

        }
    }
}

