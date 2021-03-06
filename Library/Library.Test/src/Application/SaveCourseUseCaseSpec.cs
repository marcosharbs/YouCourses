using Xunit;
using Library.Application;
using Library.Domain.AuthorAggregate.Model;
using Library.Domain.CourseAggregate.Model;
using FluentAssertions;
using Moq;
using Library.Domain.CourseAggregate.Repository;
using Library.Domain.AuthorAggregate.Repository;
using Library.Domain;

namespace Library.Test.Data
{
    public class SaveCourseUseCaseSpec
    {
        [Fact]
        public void Should_persist_course_and_author()
        {
            var author = Author.Create("Marcos Harbs", "http://marcos.harbs.com/imagem.jpeg");
            var course = Course.Create("Curso 01", "Uma descrição que passe para o curso 01", author);

            var mockCoursesRepository = new Mock<ICourseRepository>();
            mockCoursesRepository.Setup(mock => mock.AddOrUpdate(course));

            var mockAuthorRepository = new Mock<IAuthorRepository>();
            mockAuthorRepository.Setup(mock => mock.AddOrUpdate(author));

            var mockUnitOfWork = new Mock<LibraryUnitOfWork>();
            mockUnitOfWork.Setup(mock => mock.GetCourseRepository()).Returns(mockCoursesRepository.Object);
            mockUnitOfWork.Setup(mock => mock.GetAuthorRepository()).Returns(mockAuthorRepository.Object);

            var useCase = new SaveCourseUseCase(course, mockUnitOfWork.Object);
            var newCourse = useCase.Execute();

            mockCoursesRepository.Verify(mock => mock.AddOrUpdate(course), Times.Exactly(1));
            mockAuthorRepository.Verify(mock => mock.AddOrUpdate(author), Times.Exactly(1));
            newCourse.Should().Be(course);
        }
    }
}