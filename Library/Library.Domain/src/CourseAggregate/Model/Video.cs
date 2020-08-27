using System.Collections.Generic;
using Library.Domain.Common;
using Dawn;

namespace Library.Domain.CourseAggregate.Model 
{
    public sealed class Video : ValueObject<Video>
    {
        public string Name { get; }
        public string Url { get; }

        public Video(string name, string url)
        {
            Url = Guard.Argument(url, nameof(url)).NotNull().NotEmpty();
            Name = Guard.Argument(name, nameof(name)).NotNull().NotEmpty().MinLength(5);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Url;
        }
    }
}