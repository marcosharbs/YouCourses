using Xunit;
using Library.Application;
using Moq;
using FluentAssertions;
using Library.Domain.CourseAggregate.Repository;
using Library.Domain;
using Library.Domain.AuthorAggregate.Model;
using Library.Domain.CourseAggregate.Model;
using System;

namespace Library.Test.Data
{
    public class UpdateCourseUseCaseSpec
    {
        [Fact]
        public void Should_update_course()
        {
            var courseId = Guid.NewGuid();

            var author = Author.Create("Marcos Harbs", "http://marcos.harbs.com/imagem.jpeg");
            
            var course = Course.Create(courseId, "Curso 01", "Uma descrição que passe para o curso 01", author);
            course.AddVideo(Video.Create("Video 001", "https://video01.com.br"));

            var courseDB = Course.Create(courseId, "Curso 01", "Uma descrição que passe para o curso 01", author);
            courseDB.AddVideo(Video.Create("Video 001", "https://video01.com.br"));

            var mockCoursesRepository = new Mock<ICourseRepository>();
            mockCoursesRepository.Setup(mock => mock.GetById(course.Id)).Returns(courseDB);
            mockCoursesRepository.Setup(mock => mock.AddOrUpdate(course));

            var mockUnitOfWork = new Mock<LibraryUnitOfWork>();
            mockUnitOfWork.Setup(mock => mock.GetCourseRepository()).Returns(mockCoursesRepository.Object);

            var useCase = new UpdateCourseUseCase(course, mockUnitOfWork.Object);
            var updatedCourse = useCase.Execute();

            mockCoursesRepository.Verify(mock => mock.GetById(course.Id), Times.Exactly(1));
            mockCoursesRepository.Verify(mock => mock.AddOrUpdate(course), Times.Exactly(1));
            updatedCourse.Should().Be(course);
        }
    }
}