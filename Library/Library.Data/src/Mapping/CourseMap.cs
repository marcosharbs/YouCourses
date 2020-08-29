using FluentNHibernate.Mapping;
using FluentNHibernate;
using Library.Domain.CourseAggregate.Model;

namespace Library.Data.Mapping
{
    public class CourseMap : ClassMap<Course>
    {
        public CourseMap()
        {
            Id(course => course.Id).GeneratedBy.Assigned();

            Component(course => course.CourseName, course => {
                course.Map(courseName => courseName.Name).Length(255);
            });

            Component(course => course.CourseDescription, course => {
                course.Map(courseDescription => courseDescription.Description).Length(400);
            });

            References(course => course.Author);

            HasMany<Video>(Reveal.Member<Course>("_videos")).Cascade.AllDeleteOrphan();
        }
    }
}