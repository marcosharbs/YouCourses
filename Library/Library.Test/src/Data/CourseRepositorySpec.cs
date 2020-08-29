using Xunit;
using Library.Domain.CourseAggregate.Model;
using Library.Domain.AuthorAggregate.Model;
using FluentAssertions;
using System;
using System.Linq;

namespace Library.Data.Repository
{
    public class CourseRepositorySpec
    {
        [Fact]
        public void Valid_course_crud()
        {
            var unitOfWork = new NHibernateUnitOfWork();

            var authorId = Guid.NewGuid();
            var courseId = Guid.NewGuid();

            unitOfWork.BeginUnit();

            var authors = unitOfWork.Authors;
            var courses = unitOfWork.Courses;

            var author = Author.Create(authorId, "Marcos Harbs", "");
            var course = Course.Create(courseId, "Curso de JS", "Curso básico de desenvolvimento javascript", author);

            var video1 = Video.Create("Video 1", "http://youtube.com/video1");
            var video2 = Video.Create("Video 2", "http://youtube.com/video2");

            course.AddVideo(video1);
            course.AddVideo(video2);

            authors.AddOrUpdate(author);
            courses.AddOrUpdate(course);

            var courseDb = courses.GetById(courseId);

            courseDb.Id.Should().Be(course.Id);
            courseDb.Videos.Count().Should().Be(course.Videos.Count());

            unitOfWork.CommitUnit();
            unitOfWork.BeginUnit();

            authors = unitOfWork.Authors;
            courses = unitOfWork.Courses;

            course = courses.GetById(courseId);
            course.RemoveVideos();
            course.AddVideo(Video.Create("Video 3", "http://youtube.com/video1"));

            courses.AddOrUpdate(course);

            courseDb = courses.GetById(courseId);

            courseDb.Id.Should().Be(course.Id);
            courseDb.CourseName.Should().Be(course.CourseName);
            courseDb.Videos.Count().Should().Be(course.Videos.Count());

            unitOfWork.CommitUnit();
            unitOfWork.BeginUnit();

            authors = unitOfWork.Authors;
            courses = unitOfWork.Courses;

            courses.Remove(courseDb);
            authors.Remove(author);

            unitOfWork.CommitUnit();
        }
    }
}