using System;
using Microsoft.AspNetCore.Mvc;
using Truextend.Scheduling.Logic.Managers.Base;

namespace Truextend.Scheduling.Presentation.Controllers.Base
{
	[ApiController]
	public class BaseSchedulingController<T> : ControllerBase where T : class
	{
        private readonly IGenericManager<T> _classManager;

        public BaseSchedulingController(IGenericManager<T> ClassManager)
		{
			_classManager = ClassManager;
		}
	}
}

