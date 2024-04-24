using Microsoft.AspNetCore.Identity;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Truextend.Scheduling.Logic.Managers.Interfaces;
using Truextend.Scheduling.Logic.Models;
using Truextend.Scheduling.Presentation.Controllers.Base;
using Truextend.Scheduling.Presentation.Middleware;

namespace Truextend.Scheduling.Presentation.Controllers
{
	[Produces("application/json")]
	[Route("api/students")]
	public class StudentsController : BaseSchedulingController<StudentDto>
	{
        private readonly IStudentsManager _studentsManager;

        public StudentsController(IStudentsManager studentsManager) : base(studentsManager)
        {
            _studentsManager = studentsManager;
        }

        /// <summary>
        /// Add a Student to a Course.
        /// </summary>
        /// <remarks>
        /// Add a student to a respective course by providing the Id.
        /// </remarks>
        /// <param name="Id">Id of the Student who will be added to a Course</param>
        /// <param name="courseId">courseId of the Course which the Student will be added</param>
        /// <returns>
        /// The new relation between the specified Student and the Course.
        /// </returns>

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [HttpPost]
        [Route("{Id}/courses/{courseId}")]
        public async Task<ActionResult> RequestReward([FromRoute] Guid Id, [FromRoute] Guid courseId)
        {
            StudentCourseDto studentCourseDto = await _studentsManager.AddToCourse(Id, courseId);
            return Ok(new MiddlewareResponse<StudentCourseDto>(studentCourseDto));
        }
    }
}

