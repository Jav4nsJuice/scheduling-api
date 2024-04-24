using System;
using System.ComponentModel.DataAnnotations;
using Truextend.Scheduling.Data.Models.Base;

namespace Truextend.Scheduling.Data.Models
{
	public class Course : Entity
	{
        [Required]
        public string Title{ get; set; }

        public string Description { get; set; }
    }
}

