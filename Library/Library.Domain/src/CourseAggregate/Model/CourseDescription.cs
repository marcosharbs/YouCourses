using System.Collections.Generic;
using Library.Domain.Common;
using Dawn;

namespace Library.Domain.CourseAggregate.Model
{
    public class CourseDescription : ValueObject<CourseDescription>
    {
        public string Description { get; }

        protected CourseDescription() {}

        public CourseDescription(string description)
        {
            Description = Guard.Argument(description, nameof(description))
                                .NotNull()
                                .NotEmpty()
                                .MinLength(30)
                                .MaxLength(400);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Description;
        }
    }
}