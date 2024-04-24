using System;
using Microsoft.AspNetCore.Mvc;
using Truextend.Scheduling.Logic.Managers.Interfaces;
using Truextend.Scheduling.Logic.Models;
using Truextend.Scheduling.Presentation.Controllers.Base;

namespace Truextend.Scheduling.Presentation.Controllers
{
    [Produces("application/json")]
    [Route("api/courses")]
    public class CoursesController : BaseSchedulingController<CourseDto>
    {
        public CoursesController(ICoursesManager coursesManager) : base(coursesManager) { }
    }
}

