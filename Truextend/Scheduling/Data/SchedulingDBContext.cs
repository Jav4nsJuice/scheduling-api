using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Truextend.Scheduling.Data.Models;

namespace Truextend.Scheduling.Data.Repository
{
	public class SchedulingDBContext : DbContext
	{
        private readonly IConfiguration _configuration;

        public SchedulingDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

		public DbSet<Student> Student { get; set; }

        public DbSet<Course> Course { get; set; }

        public DbSet<StudentCourse> StudentCourse { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetSection("ConnectionStrings").GetSection("SchedulingConnection").Value);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(p => p.Id)
                    .HasName("PK_Student_Id");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(p => p.Id)
                    .HasName("PK_Course_Id");
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(p => p.Id)
                    .HasName("PK_StudentCourse_Id");

                entity.HasOne(p => p.Student)
                    .WithMany()
                    .HasForeignKey(p => p.StudentId)
                    .HasConstraintName("FK_StudentCourse_StudentId")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.Course)
                    .WithMany()
                    .HasForeignKey(p => p.CourseId)
                    .HasConstraintName("FK_StudentCourse_CourseId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

