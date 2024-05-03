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
	public class StudentsManager : IStudentsManager
	{
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public StudentsManager(IUnitOfWork uow, IMapper mapper)
		{
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<StudentDto> Create(StudentDto studentDto)
        {
            if (studentDto == null)
            {
                throw new BadRequestException("Fields should not be empty");
            }
            if (!studentDto.IsValid())
            {
                throw new BadRequestException("Invalid state", studentDto.GetErrors());
            }
            Student newStudent = _mapper.Map<Student>(studentDto);
            Student createResponse = await _uow.StudentRepository.CreateAsync(newStudent);
            StudentDto createdStudent = _mapper.Map<StudentDto>(createResponse);

            return createdStudent;
        }

        public async Task<bool> Delete(Guid studentId)
        {
            Student studentToDelete = await _uow.StudentRepository.GetByIdAsync(studentId)
                ?? throw new NotFoundException($"Student with ID {studentId} not found");

            await _uow.StudentRepository.DeleteAsync(studentToDelete);
            return await _uow.StudentRepository.GetByIdAsync(studentId) == null;
        }

        public async Task<IEnumerable<StudentDto>> GetAll()
        {
            IEnumerable<Student> students = await _uow.StudentRepository.GetAllAsync();
            students = students.ToList();
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task<StudentDto> GetById(Guid id)
        {
            Student studentFound = await _uow.StudentRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Student with ID {id} not found");

            StudentDto studentDto = _mapper.Map<StudentDto>(studentFound);
            return studentDto;
        }

        public async Task<StudentDto> Update(StudentDto studentDto, Guid id)
        {
            Student studentToEdit = await _uow.StudentRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Student with ID {id} not found");

            if (!studentDto.IsValid())
            {
                throw new BadRequestException("Invalid state", studentDto.GetErrors());
            }

            studentDto.Id = id;
            _mapper.Map(studentDto, studentToEdit);
            Student updateResponse = await _uow.StudentRepository.UpdateAsync(studentToEdit);
            StudentDto editedStudent = _mapper.Map<StudentDto>(updateResponse);
            return editedStudent;
        }
    }
}

