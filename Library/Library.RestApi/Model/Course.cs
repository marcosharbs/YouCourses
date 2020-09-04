using System.Collections.Generic;

namespace Library.RestApi.Model
{
    public class Course
    {
        public string Name { get; }
        public string Description { get; }
        public Author Author { get; }
        public IEnumerable<Video> Videos { get; }

        public Course(string name, string description, Author author, IEnumerable<Video> videos)
        {
            Name = name;
            Description = description;
            Author = author;
            Videos = videos;
        }

        public static Course From(Library.Domain.CourseAggregate.Model.Course course)
        {
            return new Course(course.CourseName.Name, course.CourseDescription.Description, Author.From(course.Author), Video.From(course.Videos));
        }

        public static IEnumerable<Course> From(IEnumerable<Library.Domain.CourseAggregate.Model.Course> courses)
        {
            var list = new List<Course>();

            foreach (var course in courses)
            {
                list.Add(Course.From(course));
            }

            return list;
        }
    }
}