using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Truextend.Scheduling.Data.Models;
using Truextend.Scheduling.Data.Repository.Base;

namespace Truextend.Scheduling.Data.Repository.Interfaces
{
	public interface IStudentCourseRepository : IRepository<StudentCourse>
	{
        Task<IEnumerable<StudentCourse>> GetAllByStudentIdAsync(Guid studentId);

        Task<IEnumerable<StudentCourse>> GetAllByStudentIdAndCourseIdAsync(Guid studentId, Guid courseId);
    }
}

