using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Truextend.Scheduling.Logic.Models;

namespace Truextend.Scheduling.Logic.Managers.Interfaces
{
	public interface IStudentCoursesManager
	{
        Task<IEnumerable<StudentCourseDto>> GetAll();
        Task<IEnumerable<StudentCourseDto>> GetAllByStudentId(Guid studentId);
        Task<IEnumerable<StudentCourseDto>> GetAllByCourseId(Guid courseId);
        Task<IEnumerable<StudentCourseDto>> GetAllByStudentIdAndCourseId(Guid studentId, Guid courseId);
    }
}

