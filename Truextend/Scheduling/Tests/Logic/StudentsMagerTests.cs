using System;
using System.Net.NetworkInformation;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Truextend.Scheduling.Data;
using Truextend.Scheduling.Data.Models;
using Truextend.Scheduling.Logic.Managers;
using Truextend.Scheduling.Logic.Models;
using Truextend.Scheduling.Logic.Models.Mapper;

namespace Tests.Logic
{
	public class StudentsMagerTests
	{
        private StudentsManager _studentsManager;
        private Mock<IUnitOfWork> _uowMock;

        [SetUp]
        public void Setup()
        {
            _uowMock = new Mock<IUnitOfWork>();
            IMapper mapper = GetMapper();
            _studentsManager = new StudentsManager(_uowMock.Object, mapper);
        }

        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SchedulingProfile());
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        [Test]
        public async Task Create_SingleStudent_ReturnsSameFirstName()
        {
            // Arrange
            StudentDto newStudent = new()
            {
                Id = new Guid("c8b6bc1a-dec2-45e3-a70c-0d05f4825f9a"),
                FirstName = "Javier Alejandro",
                LastName = "Ferrel Rivera"
            };

            _uowMock.Setup(u => u.StudentRepository.GetAllAsync())
                .ReturnsAsync(new List<Student> { });

            _uowMock.Setup(u => u.StudentRepository.CreateAsync(It.IsAny<Student>()))
                .ReturnsAsync(GetMapper().Map<Student>(newStudent))
                .Callback<Student>(student =>
                {
                    Assert.AreEqual(newStudent.FirstName, student.FirstName);
                    Assert.AreEqual(newStudent.LastName, student.LastName);
                });

            // Act
            var result = await _studentsManager.Create(newStudent);

            // Assert
            Assert.AreEqual(result.FirstName, newStudent.FirstName);
            Assert.AreEqual(result.LastName, newStudent.LastName);
        }

        [Test]
        public async Task Get_ManyStudents_ReturnsSameStudentsCount()
        {
            // Arrange
            var studentList = new List<Student>
            {
                new Student { Id = Guid.NewGuid(), FirstName = "Javier Alejandro", LastName = "Ferrel Rivera" },
                new Student { Id = Guid.NewGuid(), FirstName = "Ana Belén", LastName = "Vásquez Cruz" },
                new Student { Id = Guid.NewGuid(), FirstName = "Jorge Mauricio", LastName = "Peredo Vargas" }
            };

            _uowMock.Setup(u => u.StudentRepository.GetAllAsync())
                .ReturnsAsync(studentList);

            // Act
            var result = await _studentsManager.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(studentList.Count, result.Count());
        }

        [Test]
        public async Task Get_SingleStudent_ReturnSameName()
        {
            // Arrange
            var studentId = Guid.NewGuid();
            var expectedStudentDto = new StudentDto { Id = studentId, FirstName = "Javier Alejandro", LastName = "Ferrel Rivera" };

            _uowMock.Setup(u => u.StudentRepository.GetByIdAsync(studentId))
                .ReturnsAsync(new Student { Id = studentId, FirstName = "Javier Alejandro", LastName = "Ferrel Rivera" });

            // Act
            var result = await _studentsManager.GetById(studentId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedStudentDto.Id, result.Id);
            Assert.AreEqual(expectedStudentDto.FirstName, result.FirstName);
        }

        [Test]
        public async Task Update_ExistingStudent_ReturnsUpdatedStudent()
        {
            // Arrange
            var studentId = Guid.NewGuid();
            var updatedStudentDto = new StudentDto { Id = studentId, FirstName = "Updated", LastName = "Student" };

            _uowMock.Setup(u => u.StudentRepository.GetByIdAsync(studentId))
                .ReturnsAsync(new Student { Id = studentId, FirstName = "Existing", LastName = "Student" });

            _uowMock.Setup(u => u.StudentRepository.UpdateAsync(It.IsAny<Student>()))
                .ReturnsAsync(GetMapper().Map<Student>(updatedStudentDto));

            // Act
            var result = await _studentsManager.Update(updatedStudentDto, studentId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedStudentDto.FirstName, result.FirstName);
            Assert.AreEqual(updatedStudentDto.LastName, result.LastName);
        }

        [Test]
        public async Task Delete_StudentExists_StudentDeleted()
        {
            // Arrange
            var studentIdToDelete = Guid.NewGuid();

            _uowMock.Setup(u => u.StudentRepository.GetByIdAsync(studentIdToDelete))
                .ReturnsAsync(new Student { Id = studentIdToDelete, FirstName = "Student To", LastName = "Delete" });

            _uowMock.Setup(u => u.StudentRepository.DeleteAsync(It.IsAny<Student>()))
                .ReturnsAsync(new Student());

            // Act
            await _studentsManager.Delete(studentIdToDelete);

            // Assert
            _uowMock.Verify(u => u.StudentRepository.DeleteAsync(It.IsAny<Student>()), Times.Once);
        }
    }
}

