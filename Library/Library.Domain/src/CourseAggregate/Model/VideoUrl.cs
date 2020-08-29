using System.Collections.Generic;
using Library.Domain.Common;
using Dawn;

namespace Library.Domain.CourseAggregate.Model
{
    public class VideoUrl : ValueObject<VideoUrl>
    {
        public string Url { get; }

        protected VideoUrl() {}

        public VideoUrl(string url)
        {
            Url = Guard.Argument(url, nameof(url)).NotNull().NotEmpty();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Url;
        }
    }
}