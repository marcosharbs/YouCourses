using System;
using Library.Domain.Common;

namespace Library.Domain.CourseAggregate.Model 
{
    public class Video : Entity
    {
        public virtual VideoName VideoName { get; }
        public virtual VideoUrl VideoUrl { get; }

        protected Video() {}

        private Video(Guid id, VideoName videoName, VideoUrl videoUrl) : base(id)
        {
            VideoName = videoName;
            VideoUrl = videoUrl;
        }

        public virtual bool HasSameUrl(Video otherVideo)
        {
            return VideoUrl.Url.Equals(otherVideo.VideoUrl.Url);
        }

        public static Video Create(Guid id, string name, string url)
        {
            return new Video(id, new VideoName(name), new VideoUrl(url));
        }

        public static Video Create(string name, string url)
        {
            return new Video(Guid.NewGuid(), new VideoName(name), new VideoUrl(url));
        }
    }
}