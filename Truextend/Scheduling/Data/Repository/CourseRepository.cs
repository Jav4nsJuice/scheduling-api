using System;
using Truextend.Scheduling.Data.Models;
using Truextend.Scheduling.Data.Repository.Base;
using Truextend.Scheduling.Data.Repository.Interfaces;

namespace Truextend.Scheduling.Data.Repository
{
	public class CourseRepository : Repository<Course>, ICourseRepository
	{
		public CourseRepository(SchedulingDBContext schedulingDbContext) : base(schedulingDbContext) { }
	}
}

