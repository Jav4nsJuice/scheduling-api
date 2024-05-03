using System;
using System.Collections.Generic;

namespace Truextend.Scheduling.Logic.Models.Validation
{
	public abstract class BaseValidatable : IValidatable
	{
        protected IEnumerable<ValidationError> Errors;

        public BaseValidatable()
		{
            Errors = new List<ValidationError>();
        }

        public IEnumerable<ValidationError> GetErrors()
        {
            return Errors;
        }

        public abstract bool IsValid();

        protected void AddError(ValidationError error)
        {
            ((List<ValidationError>)Errors).Add(error);
        }
    }
}

