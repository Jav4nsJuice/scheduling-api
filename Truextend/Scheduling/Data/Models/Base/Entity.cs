using System;
using System.ComponentModel.DataAnnotations;

namespace Truextend.Scheduling.Data.Models.Base
{
	public class Entity
	{
        [Key]
        [Required]
        public Guid Id { get; set; }
    }
}

