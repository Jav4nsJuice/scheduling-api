using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Truextend.Scheduling.Data.Models;
using Truextend.Scheduling.Data.Repository.Base;
using Truextend.Scheduling.Data.Repository.Interfaces;

namespace Truextend.Scheduling.Data.Repository
{
	public class StudentCourseRepository : Repository<StudentCourse>, IStudentCourseRepository
	{
        protected readonly SchedulingDBContext _schedulingDbContext;
        public StudentCourseRepository(SchedulingDBContext schedulingDbContext) : base(schedulingDbContext)
        {
            _schedulingDbContext = schedulingDbContext;
        }

        public async Task<IEnumerable<StudentCourse>> GetAllByStudentIdAndCourseIdAsync(Guid studentId, Guid courseId)
        {
            return await _schedulingDbContext
               .Set<StudentCourse>()
               .Where(u => u.StudentId == studentId && u.CourseId == courseId)
               .ToListAsync();
        }

        public async Task<IEnumerable<StudentCourse>> GetAllByStudentIdAsync(Guid studentId)
        {
            return await _schedulingDbContext
                .Set<StudentCourse>()
                .Where(u => u.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<StudentCourse>> GetStudentsOnCourse(Guid courseId)
        {
            var studentCourse = await GetAllAsync();
            studentCourse = studentCourse.Where(sc => sc.CourseId == courseId);
            return studentCourse;
        }
    }
}

