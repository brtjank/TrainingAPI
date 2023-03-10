// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrainingApp.Entities;

#nullable disable

namespace TrainingApp.Migrations
{
    [DbContext(typeof(TrainingDbContext))]
    [Migration("20230131152132_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("TrainingApp.Entities.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Load")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Reps")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SessionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Sets")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SessionId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("TrainingApp.Entities.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("Duration")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Intensity")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TraineeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TraineeId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("TrainingApp.Entities.Trainee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Team")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Trainees");
                });

            modelBuilder.Entity("TrainingApp.Entities.Exercise", b =>
                {
                    b.HasOne("TrainingApp.Entities.Session", "Session")
                        .WithMany("Exercises")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Session");
                });

            modelBuilder.Entity("TrainingApp.Entities.Session", b =>
                {
                    b.HasOne("TrainingApp.Entities.Trainee", "Trainee")
                        .WithMany("Sessions")
                        .HasForeignKey("TraineeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trainee");
                });

            modelBuilder.Entity("TrainingApp.Entities.Session", b =>
                {
                    b.Navigation("Exercises");
                });

            modelBuilder.Entity("TrainingApp.Entities.Trainee", b =>
                {
                    b.Navigation("Sessions");
                });
#pragma warning restore 612, 618
        }
    }
}
