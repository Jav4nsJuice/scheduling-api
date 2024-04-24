using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Truextend.Scheduling.Data.Models.Base;

namespace Truextend.Scheduling.Data.Models
{
    [Table("StudentCourse")]
    public class StudentCourse : Entity
	{
        [Required]
        public Guid StudentId { get; set; }

        [Required]
        public Guid CourseId { get; set; }

        public virtual Course Course { get; set; }

        public virtual Student Student { get; set; }
    }
}

