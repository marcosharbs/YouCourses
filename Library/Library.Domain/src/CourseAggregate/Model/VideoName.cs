using System.Collections.Generic;
using Library.Domain.Common;
using Dawn;

namespace Library.Domain.CourseAggregate.Model
{
    public class VideoName : ValueObject<VideoName>
    {
        public string Name { get; }

        protected VideoName() {}

        public VideoName(string name)
        {
            Name = Guard.Argument(name, nameof(name)).NotNull().NotEmpty().MinLength(5);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}