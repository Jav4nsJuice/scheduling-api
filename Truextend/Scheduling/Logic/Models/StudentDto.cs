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
            if (!regex.IsMatch(FirstName) || string.IsNullOrWhiteSpace(FirstName))
            {
                AddError(new ValidationError
                {
                    Field = "FirstName",
                    Message = "FirstName can't have numbers, special characters or white spaces."
                });
            }
            if (!regex.IsMatch(LastName) || string.IsNullOrWhiteSpace(FirstName))
            {
                AddError(new ValidationError
                {
                    Field = "LastName",
                    Message = "LastName can't have numbers, special characters or white spaces."
                });
            }
            return !Errors.Any();
        }
    }
}

