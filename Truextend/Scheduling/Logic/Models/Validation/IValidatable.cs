using System;
using System.Collections.Generic;

namespace Truextend.Scheduling.Logic.Models.Validation
{
	public interface IValidatable
	{
        bool IsValid();

        IEnumerable<ValidationError> GetErrors();
    }
}

