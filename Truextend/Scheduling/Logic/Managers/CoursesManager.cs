using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Truextend.Scheduling.Data;
using Truextend.Scheduling.Data.Models;
using Truextend.Scheduling.Logic.Exceptions;
using Truextend.Scheduling.Logic.Managers.Interfaces;
using Truextend.Scheduling.Logic.Models;

namespace Truextend.Scheduling.Logic.Managers
{
	public class CoursesManager : ICoursesManager
	{
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CoursesManager(IUnitOfWork uow, IMapper mapper)
		{
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<CourseDto> Create(CourseDto courseDto)
        {
            if (courseDto == null)
            {
                throw new BadRequestException("Fields should not be empty");
            }
            if (!courseDto.IsValid())
            {
                throw new BadRequestException("Invalid state", courseDto.GetErrors());
            }
            Course newCourse = _mapper.Map<Course>(courseDto);
            Course createResponse = await _uow.CourseRepository.CreateAsync(newCourse);
            CourseDto createdCourse = _mapper.Map<CourseDto>(createResponse);

            return createdCourse;
        }

        public async Task<bool> Delete(Guid courseId)
        {
            Course courseToDelete = await _uow.CourseRepository.GetByIdAsync(courseId)
                ?? throw new NotFoundException($"Course with ID {courseId} not found");

            await _uow.CourseRepository.DeleteAsync(courseToDelete);
            return await _uow.CourseRepository.GetByIdAsync(courseId) == null;
        }

        public async Task<IEnumerable<CourseDto>> GetAll()
        {
            IEnumerable<Course> courses = await _uow.CourseRepository.GetAllAsync();
            courses = courses.ToList();
            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }

        public async Task<CourseDto> GetById(Guid id)
        {
            Course courseFound = await _uow.CourseRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Course with ID {id} not found");

            CourseDto courseDto = _mapper.Map<CourseDto>(courseFound);
            return courseDto;
        }

        public async Task<CourseDto> Update(CourseDto courseDto, Guid id)
        {
            Course courseToEdit = await _uow.CourseRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Course with ID {id} not found");

            if (!courseDto.IsValid())
            {
                throw new BadRequestException("Invalid state", courseDto.GetErrors());
            }

            courseDto.Id = id;
            _mapper.Map(courseDto, courseToEdit);
            Course updateResponse = await _uow.CourseRepository.UpdateAsync(courseToEdit);
            CourseDto editedCourse = _mapper.Map<CourseDto>(updateResponse);
            return editedCourse;
        }
    }
}

