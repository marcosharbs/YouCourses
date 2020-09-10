using Xunit;
using Library.Domain.CourseAggregate.Repository;
using Library.Application;
using Moq;
using Library.Domain;
using System.Collections.Generic;
using Library.Domain.CourseAggregate.Model;
using Library.Domain.AuthorAggregate.Model;
using FluentAssertions;

namespace Library.Test.Data
{
    public class GetCoursesUseCaseSpec
    {
        [Fact]
        public void Should_get_courses()
        {
            var author = Author.Create("Marcos Harbs", "http://marcos.harbs.com/imagem.jpeg");
            
            var coursesList = new List<Course>();
            coursesList.Add(Course.Create("Curso 01", "Uma descrição que passe para o curso 01", author));
            coursesList.Add(Course.Create("Curso 02", "Uma descrição que passe para o curso 02", author));
            coursesList.Add(Course.Create("Curso 03", "Uma descrição que passe para o curso 03", author));

            var mockCoursesRepository = new Mock<ICourseRepository>();
            mockCoursesRepository.Setup(mock => mock.GetPartial(0, 20)).Returns(coursesList);

            var mockUnitOfWork = new Mock<ILibraryUnitOfWork>();
            mockUnitOfWork.SetupGet(mock => mock.Courses).Returns(mockCoursesRepository.Object);

            var useCase = new GetCoursesUseCase(0, 20, mockUnitOfWork.Object);
            var courses = useCase.Execute();

            mockUnitOfWork.Verify(mock => mock.BeginUnit(), Times.Exactly(1));
            mockUnitOfWork.Verify(mock => mock.CommitUnit(), Times.Exactly(1));
            mockUnitOfWork.Verify(mock => mock.RollbackUnit(), Times.Never());
            mockCoursesRepository.Verify(mock => mock.GetPartial(0, 20), Times.Exactly(1));
            courses.Should().BeEquivalentTo(coursesList);
        }
    }
}