using FluentNHibernate.Mapping;
using Library.Domain.CourseAggregate.Model;

namespace Library.Data.Mapping
{
    public class CourseMap : ClassMap<Course>
    {
        public CourseMap()
        {
            Id(course => course.Id).GeneratedBy.Guid();

            Component(course => course.CourseName, course => {
                course.Map(courseName => courseName.Name).Length(255);
            });

            Component(course => course.CourseDescription, course => {
                course.Map(courseDescription => courseDescription.Description).Length(400);
            });

            References(course => course.Author);

            HasMany(course => course.Videos).Cascade.All();
        }
    }
}