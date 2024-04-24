using System;
using System.Linq;
using Truextend.Scheduling.Logic.Models.Validation;

namespace Truextend.Scheduling.Logic.Models
{
	public class CourseDto : BaseValidatable, IValidatable
	{
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public override bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                AddError(new ValidationError
                {
                    Field = "Title",
                    Message = "Title can't be created with white spaces."
                });
            }
            return !Errors.Any();
        }
    }
}

