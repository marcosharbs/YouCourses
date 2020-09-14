using Library.Domain;
using Library.Domain.CourseAggregate.Model;
using Core.Application;

namespace Library.Application
{
    public class SaveCourseUseCase : UseCase<Course, ILibraryUnitOfWork>
    {
        private readonly Course _course;

        public SaveCourseUseCase(Course course,
                                 ILibraryUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _course = course;
        }

        protected override Course Action()
        {
            _unitOfWork.Authors.AddOrUpdate(_course.Author);
            _unitOfWork.Courses.AddOrUpdate(_course);
            return _course;
        }
    }
}