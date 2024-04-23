using System;
using System.ComponentModel.DataAnnotations;
using Truextend.Scheduling.Data.Models.Base;

namespace Truextend.Scheduling.Data.Models
{
	public class Student : Entity
	{
		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }
	}
}

