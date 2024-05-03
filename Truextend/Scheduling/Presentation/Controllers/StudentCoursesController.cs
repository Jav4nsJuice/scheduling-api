using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Truextend.Scheduling.Logic.Managers.Interfaces;
using Truextend.Scheduling.Logic.Models;
using Truextend.Scheduling.Presentation.Middleware;

namespace Truextend.Scheduling.Presentation.Controllers
{
    [Produces("application/json")]
    [Route("api/students/courses")]
    public class StudentCoursesController : Controller
	{
        private readonly IStudentCoursesManager _studentCoursesManager;

        public StudentCoursesController(IStudentCoursesManager studentCoursesManager)
		{
            _studentCoursesManager = studentCoursesManager;
        }

        /// <summary>
        /// Get all Students Courses registries.
        /// </summary>
        /// <remarks>
        /// Get information about the courses of all Students.
        /// </remarks>
        /// <returns>
        /// A list of all Students Courses registered.
        /// </returns>

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [HttpGet]
        [Route("")]
        public async Task<ActionResult> GetAllAsync()
        {
            IEnumerable<StudentCourseDto> studentCoursesDto = await _studentCoursesManager.GetAll();
            return Ok(new MiddlewareResponse<IEnumerable<StudentCourseDto>>(studentCoursesDto));
        }

        /// <summary>
        /// Get all Students Courses registries from an specific Student.
        /// </summary>
        /// <remarks>
        /// Get information about the courses of one student.
        /// </remarks>
        /// <param name="Id">Id of the Student whose information is required.</param>
        /// <returns>
        /// A list of all the Courses registered from a Student.
        /// </returns>

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [HttpGet]
        [Route("student/{Id}")]
        public async Task<ActionResult> GetAllByStudentIdAsync([FromRoute] Guid Id)
        {
            IEnumerable<StudentCourseDto> studentCoursesDto = await _studentCoursesManager.GetAllByStudentId(Id);
            return Ok(new MiddlewareResponse<IEnumerable<StudentCourseDto>>(studentCoursesDto));
        }

        /// <summary>
        /// Get all Students from an specific Course.
        /// </summary>
        /// <remarks>
        /// Get all of the Students by a given CourseId.
        /// </remarks>
        /// <param name="Id">Id of the Course.</param>
        /// <returns>
        /// A list of all the Students registered to a Course.
        /// </returns>

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [HttpGet]
        [Route("course/{Id}")]
        public async Task<ActionResult> GetAllByCourseIdAsync([FromRoute] Guid Id)
        {
            IEnumerable<StudentCourseDto> studentCoursesDto = await _studentCoursesManager.GetAllByCourseId(Id);
            return Ok(new MiddlewareResponse<IEnumerable<StudentCourseDto>>(studentCoursesDto));
        }

        /// <summary>
        /// Get all Students Courses registries from an specific Student and an specific Course.
        /// </summary>
        /// <remarks>
        /// Get information about the Courses of an Student.
        /// </remarks>
        /// <param name="studentId">Id of the Student who attends to a Course.</param>
        /// <param name="courseId">Id of the Course attended.</param>
        /// <returns>
        /// A history from the steps of the User's Reward
        /// </returns>

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [HttpGet]
        [Route("student/{studentId}/course/{courseId}")]
        public async Task<ActionResult> GetAllByStudentIdAndCourseIdAsync([FromRoute] Guid studentId, [FromRoute] Guid courseId)
        {
            IEnumerable<StudentCourseDto> studentCoursesDto = await _studentCoursesManager.GetAllByStudentIdAndCourseId(studentId, courseId);
            return Ok(new MiddlewareResponse<IEnumerable<StudentCourseDto>>(studentCoursesDto));
        }
    }
}

