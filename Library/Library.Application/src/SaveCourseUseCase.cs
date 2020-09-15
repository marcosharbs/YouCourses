using Library.Domain;
using Library.Domain.CourseAggregate.Model;
using Core.Application;

namespace Library.Application
{
    public class SaveCourseUseCase : UseCase<Course, LibraryUnitOfWork>
    {
        private readonly Course _course;

        public SaveCourseUseCase(Course course,
                                 LibraryUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _course = course;
        }

        protected override Course Action()
        {
            _unitOfWork.GetAuthorRepository().AddOrUpdate(_course.Author);
            _unitOfWork.GetCourseRepository().AddOrUpdate(_course);
            return _course;
        }
    }
}