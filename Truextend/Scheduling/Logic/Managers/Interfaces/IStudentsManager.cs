using System;
using System.Threading.Tasks;
using Truextend.Scheduling.Logic.Managers.Base;
using Truextend.Scheduling.Logic.Models;

namespace Truextend.Scheduling.Logic.Managers.Interfaces
{
	public interface IStudentsManager : IGenericManager<StudentDto>
	{
        Task<StudentCourseDto> AddToCourse(Guid studentId, Guid courseId);
    }
}

