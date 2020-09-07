using System.Collections.Generic;
using System;
using Library.Domain.CourseAggregate.Model;

namespace Library.RestApi.Model
{
    public class VideoApi
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public VideoApi() { }

        private VideoApi(Guid id, string name, string url)
        {
            Id = id;
            Name = name;
            Url = url;
        }

        public static VideoApi FromDomain(Video video)
        {
            return new VideoApi(video.Id, video.VideoName.Name, video.VideoUrl.Url);
        }

        public static IEnumerable<VideoApi> FromDomain(IEnumerable<Video> videos)
        {
            var list = new List<VideoApi>();

            foreach (var video in videos)
            {
                list.Add(VideoApi.FromDomain(video));
            }

            return list;
        }

        public static Video ToDomain(VideoApi video)
        {
            return Library.Domain.CourseAggregate.Model.Video.Create(video.Id, video.Name, video.Url);
        }

        public static IEnumerable<Video> ToDomain(IEnumerable<VideoApi> videos)
        {
            var list = new List<Video>();

            foreach (var video in videos)
            {
                list.Add(VideoApi.ToDomain(video));
            }

            return list;
        }
    }
}