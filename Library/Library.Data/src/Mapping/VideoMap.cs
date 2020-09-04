using FluentNHibernate.Mapping;
using Library.Domain.CourseAggregate.Model;

namespace Library.Data.Mapping
{
    public class VideoMap : ClassMap<Video>
    {
        public VideoMap()
        {
            Id(video => video.Id).GeneratedBy.Assigned();

            Component(video => video.VideoName, video => {
                video.Map(videoName => videoName.Name).Length(255);
            }).Not.LazyLoad();

            Component(video => video.VideoUrl, video => {
                video.Map(videoUrl => videoUrl.Url).Length(400);
            }).Not.LazyLoad();
        }
    }
}