using System.Collections.Generic;
using Library.Domain.Common;
using Dawn;

namespace Library.Domain.CourseAggregate.Model
{
    public class CourseName : ValueObject<CourseName>
    {
        public string Name { get; }

        protected CourseName() {}

        public CourseName(string name)
        {
            Name = Guard.Argument(name, nameof(name))
                        .NotEmpty()
                        .NotNull()
                        .MinLength(5);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}