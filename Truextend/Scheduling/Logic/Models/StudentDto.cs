using System;
using System.Linq;
using System.Text.RegularExpressions;
using Truextend.Scheduling.Logic.Models.Validation;

namespace Truextend.Scheduling.Logic.Models
{
	public class StudentDto : BaseValidatable, IValidatable
	{
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override bool IsValid()
        {
            Regex regex = new Regex(@"^[a-zA-Z\s]+$");
            if (!regex.IsMatch(FirstName))
            {
                AddError(new ValidationError
                {
                    Field = "FirstName",
                    Message = "FirstName can't have numbers or special characters."
                });
            }
            return !Errors.Any();
        }
    }
}

