using System.Collections.Generic;
using System;
using Dawn;
using Core.Domain;
using Library.Domain.AuthorAggregate.Model;
using System.Linq;

namespace Library.Domain.CourseAggregate.Model
{
    public class Course : AggregateRoot
    {
        public virtual CourseName CourseName { get; protected set; }
        public virtual CourseDescription CourseDescription { get; protected set; }
        public virtual Author Author { get; }
        protected virtual IList<Video> _videos { get; set; }
        public virtual IEnumerable<Video> Videos
        {
            get
            {
                return _videos.AsEnumerable();
            }
        }

        protected Course() {}

        private Course(Guid id, 
                       CourseName courseName, 
                       CourseDescription courseDescription,
                       Author author, 
                       List<Video> videos) : base(id)
        {
            CourseName = courseName;
            CourseDescription = courseDescription;
            Author = Guard.Argument(author, nameof(author)).NotNull();
            _videos = new List<Video>();
            Guard.Argument(videos, nameof(videos)).NotNull();
            videos.ForEach(video => AddVideo(video));
        }

        public virtual void UpdateName(string name)
        {
            CourseName = new CourseName(name);
        }

        public virtual void UpdateDescription(string description)
        {
            CourseDescription = new CourseDescription(description);
        }

        public virtual void AddVideo(Video newVideo)
        {
            if(_videos.FirstOrDefault(video => video.HasSameUrl(newVideo)) != null)
                throw new InvalidOperationException();

            _videos.Add(newVideo);
        }

        public virtual void RemoveVideos()
        {
            _videos.Clear();
        }

        public static Course Create(Guid id, string name, string description, Author author, List<Video> videos)
        {
            return new Course(
                id, 
                new CourseName(name), 
                new CourseDescription(description), 
                author,
                videos
            );
        }

        public static Course Create(Guid id, string name, string description, Author author)
        {
            return Course.Create(id, name, description, author, new List<Video>());
        }

        public static Course Create(string name, string description, Author author, List<Video> videos)
        {
            return Course.Create(Guid.NewGuid(), name, description, author, videos);
        }

        public static Course Create(string name, string description, Author author)
        {
            return Course.Create(name, description, author, new List<Video>());
        }

    }
}