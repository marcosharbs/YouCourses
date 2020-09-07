using System.Collections.Generic;
using System.Linq;
using System;
using Library.Domain.CourseAggregate.Model;

namespace Library.RestApi.Model
{
    public class CourseApi
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AuthorApi Author { get; set; }
        public IEnumerable<VideoApi> Videos { get; set; }

        public CourseApi() { }

        private CourseApi(Guid id, string name, string description, AuthorApi author, IEnumerable<VideoApi> videos)
        {
            Id = id;
            Name = name;
            Description = description;
            Author = author;
            Videos = videos;
        }

        public static CourseApi From(Course course)
        {
            return new CourseApi(
                course.Id, 
                course.CourseName.Name, 
                course.CourseDescription.Description, 
                AuthorApi.From(course.Author), 
                VideoApi.From(course.Videos));
        }

        public static IEnumerable<CourseApi> From(IEnumerable<Course> courses)
        {
            var list = new List<CourseApi>();

            foreach (var course in courses)
            {
                list.Add(CourseApi.From(course));
            }

            return list;
        }

        public static Course To(CourseApi course)
        {
            return Library.Domain.CourseAggregate.Model.Course.Create(
                course.Id,
                course.Name, 
                course.Description, 
                AuthorApi.To(course.Author),
                VideoApi.To(course.Videos).ToList());
        }
    }
}