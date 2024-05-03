using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Truextend.Scheduling.Data;
using Truextend.Scheduling.Data.Models;
using Truextend.Scheduling.Logic.Exceptions;
using Truextend.Scheduling.Logic.Managers.Interfaces;
using Truextend.Scheduling.Logic.Models;

namespace Truextend.Scheduling.Logic.Managers
{
	public class StudentCoursesManager : IStudentCoursesManager
	{
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public StudentCoursesManager(IUnitOfWork uow, IMapper mapper)
		{
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentCourseDto>> GetAll()
        {
            IEnumerable<StudentCourse> studentCourses = await _uow.StudentCourseRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StudentCourseDto>>(studentCourses);
        }

        public async Task<IEnumerable<StudentCourseDto>> GetAllByStudentId(Guid studentId)
        {
            _ = await _uow.StudentRepository.GetByIdAsync(studentId)
                ?? throw new NotFoundException($"Student with studentId {studentId} not found");

            IEnumerable<StudentCourse> studentCourses = await _uow.StudentCourseRepository.GetAllByStudentIdAsync(studentId);
            return _mapper.Map<IEnumerable<StudentCourseDto>>(studentCourses);
        }

        public async Task<IEnumerable<StudentCourseDto>> GetAllByStudentIdAndCourseId(Guid studentId, Guid courseId)
        {
            _ = await _uow.StudentRepository.GetByIdAsync(studentId)
                ?? throw new NotFoundException($"Student with StudentId {studentId} not found");
            _ = await _uow.CourseRepository.GetByIdAsync(courseId)
                ?? throw new NotFoundException($"Course with CourseId {courseId} not found");

            IEnumerable<StudentCourse> studentCourses = await _uow.StudentCourseRepository.GetAllByStudentIdAndCourseIdAsync(studentId, courseId);

            return _mapper.Map<IEnumerable<StudentCourseDto>>(studentCourses); ;
        }
    }
}

