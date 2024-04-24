﻿using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Truextend.Scheduling.Data.Models;

namespace Truextend.Scheduling.Data.Repository
{
	public class SchedulingDBContext : DbContext
	{
        private readonly IConfiguration _configuration;

        public SchedulingDBContext(DbContextOptions<SchedulingDBContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

		public DbSet<Student> Student { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SchedulingConnection"));
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

            base.OnModelCreating(modelBuilder);
        }
    }
}
