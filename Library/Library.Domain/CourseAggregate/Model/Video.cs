using System;
using System.Collections.Generic;
using Library.Domain.Common;

namespace Library.Domain.CourseAggregate.Model 
{
    public sealed class Video : ValueObject<Video>
    {
        public string Name { get; }
        public string Url { get; }

        public Video(string name, string url)
        {
            if(string.IsNullOrEmpty(name))
                throw new InvalidOperationException();

            if(string.IsNullOrEmpty(url))
                throw new InvalidOperationException();

            if(name.Length < 5)
                throw new InvalidOperationException();

            Url = url;
            Name = name;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Url;
        }
    }
}