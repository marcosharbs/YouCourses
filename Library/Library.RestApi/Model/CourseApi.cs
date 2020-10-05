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

        private CourseApi(Guid id,
                          string name, 
                          string description, 
                          AuthorApi author, 
                          IEnumerable<VideoApi> videos)
        {
            Id = id;
            Name = name;
            Description = description;
            Author = author;
            Videos = videos;
        }

        public static CourseApi FromDomain(Course course)
        {
            return new CourseApi(
                course.Id, 
                course.CourseName.Name, 
                course.CourseDescription.Description, 
                AuthorApi.FromDomain(course.Author), 
                VideoApi.FromDomain(course.Videos));
        }

        public static IEnumerable<CourseApi> FromDomain(IEnumerable<Course> courses)
        {
            var list = new List<CourseApi>();

            foreach (var course in courses)
            {
                list.Add(CourseApi.FromDomain(course));
            }

            return list;
        }

        public static Course ToDomain(CourseApi course)
        {
            return Library.Domain.CourseAggregate.Model.Course.Create(
                course.Id,
                course.Name, 
                course.Description, 
                AuthorApi.ToDomain(course.Author),
                VideoApi.ToDomain(course.Videos).ToList());
        }
    }
}