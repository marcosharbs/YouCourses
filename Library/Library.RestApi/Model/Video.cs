using System.Collections.Generic;

namespace Library.RestApi.Model
{
    public class Video
    {
        public string Name { get; }
        public string Url { get; }

        public Video(string name, string url)
        {
            Name = name;
            Url = url;
        }

        public static Video From(Library.Domain.CourseAggregate.Model.Video video)
        {
            return new Video(video.VideoName.Name, video.VideoUrl.Url);
        }

        public static IEnumerable<Video> From(IEnumerable<Library.Domain.CourseAggregate.Model.Video> videos)
        {
            var list = new List<Video>();

            foreach (var video in videos)
            {
                list.Add(Video.From(video));
            }

            return list;
        }
    }
}