using Xunit;
using Library.Domain.CourseAggregate.Model;
using Library.Domain.AuthorAggregate.Model;
using FluentAssertions;
using System;
using System.Linq;

namespace Library.Data.Repository
{
    [Collection("Database")]
    public class CourseRepositorySpec
    {
        [Fact]
        public void Valid_course_crud()
        {
            var appsettings = Config.InitConfiguration();

            var unitOfWork = new NHibernateUnitOfWork(SessionHelper.GetSessionFactory(appsettings.GetSection("Database")["ConnectionSrtring"]));

            var authorId = Guid.NewGuid();
            var courseId = Guid.NewGuid();

            unitOfWork.BeginUnit();

            var authors = unitOfWork.GetAuthorRepository();
            var courses = unitOfWork.GetCourseRepository();

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

            authors = unitOfWork.GetAuthorRepository();
            courses = unitOfWork.GetCourseRepository();

            course = courses.GetById(courseId);
            course.RemoveVideos();
            course.AddVideo(Video.Create("Video 3", "http://youtube.com/video1"));

            courses.AddOrUpdate(course);

            courseDb = courses.GetById(courseId);

            courseDb.Id.Should().Be(course.Id);
            courseDb.CourseName.Should().Be(course.CourseName);
            courseDb.Videos.Count().Should().Be(course.Videos.Count());

            var coursesCount = courses.Count();

            coursesCount.Should().Be(1);

            var allCourses = courses.GetAll();
            var partialCourses = courses.GetPartial(0, 20);

            allCourses.Count.Should().Be(1);
            partialCourses.Count.Should().Be(1);
            allCourses.ToList()[0].Should().Be(courseDb);
            partialCourses.ToList()[0].Should().Be(courseDb);

            unitOfWork.CommitUnit();
            unitOfWork.BeginUnit();

            authors = unitOfWork.GetAuthorRepository();
            courses = unitOfWork.GetCourseRepository();

            courses.Remove(courseDb);
            authors.Remove(author);

            unitOfWork.CommitUnit();
        }
    }
}