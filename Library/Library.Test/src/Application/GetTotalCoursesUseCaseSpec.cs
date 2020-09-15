using Xunit;
using Library.Domain.CourseAggregate.Repository;
using Library.Application;
using Moq;
using Library.Domain;
using FluentAssertions;

namespace Library.Test.Data
{
    public class GetTotalCoursesUseCaseSpec
    {
        [Fact]
        public void Should_count_courses()
        {
            var mockCoursesRepository = new Mock<ICourseRepository>();
            mockCoursesRepository.Setup(mock => mock.Count()).Returns(5);

            var mockUnitOfWork = new Mock<LibraryUnitOfWork>();
            mockUnitOfWork.Setup(mock => mock.GetCourseRepository()).Returns(mockCoursesRepository.Object);

            var useCase = new GetTotalCoursesUseCase(mockUnitOfWork.Object);
            var totalCourses = useCase.Execute();

            mockCoursesRepository.Verify(mock => mock.Count(), Times.Exactly(1));
            totalCourses.Should().Be(5);
        }
    }
}