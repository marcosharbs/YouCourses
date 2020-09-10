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

            var mockUnitOfWork = new Mock<ILibraryUnitOfWork>();
            mockUnitOfWork.SetupGet(mock => mock.Courses).Returns(mockCoursesRepository.Object);

            var useCase = new GetTotalCoursesUseCase(mockUnitOfWork.Object);
            var totalCourses = useCase.Execute();

            mockUnitOfWork.Verify(mock => mock.BeginUnit(), Times.Exactly(1));
            mockUnitOfWork.Verify(mock => mock.CommitUnit(), Times.Exactly(1));
            mockUnitOfWork.Verify(mock => mock.RollbackUnit(), Times.Never());
            mockCoursesRepository.Verify(mock => mock.Count(), Times.Exactly(1));
            totalCourses.Should().Be(5);
        }
    }
}