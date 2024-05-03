using Microsoft.AspNetCore.Mvc;
using Truextend.Scheduling.Logic.Managers.Interfaces;
using Truextend.Scheduling.Logic.Models;
using Truextend.Scheduling.Presentation.Controllers.Base;

namespace Truextend.Scheduling.Presentation.Controllers
{
	[Produces("application/json")]
	[Route("api/students")]
	public class StudentsController : BaseSchedulingController<StudentDto>
	{
        public StudentsController(IStudentsManager studentsManager) : base(studentsManager) { }
    }
}

