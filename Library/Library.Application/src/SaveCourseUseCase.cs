using Library.Domain.Common;
using Library.Domain.CourseAggregate.Model;

namespace Library.Application
{
    public class SaveCourseUseCase : UseCase<Course>
    {
        private Course _course;

        public SaveCourseUseCase(Course course,
                                 IUnitOfWork unitOfWork) : base(unitOfWork)
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