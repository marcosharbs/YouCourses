using Library.Domain.Common;
using Library.Domain.CourseAggregate.Model;
using System.Collections.Generic;

namespace Library.Application
{
    public class GetCoursesUseCase : UseCase<ICollection<Course>>
    {

        public GetCoursesUseCase(IUnitOfWork unitOfWork) : base(unitOfWork) {}

        protected override ICollection<Course> Action()
        {
            return _unitOfWork.Courses.GetAll();
        }
    }
}