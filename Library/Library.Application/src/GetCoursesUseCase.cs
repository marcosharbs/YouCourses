using Library.Domain.Common;
using Library.Domain.CourseAggregate.Model;
using System.Collections.Generic;

namespace Library.Application
{
    public class GetCoursesUseCase : UseCase<ICollection<Course>>
    {
        private int _page;

        public GetCoursesUseCase(int page, IUnitOfWork unitOfWork) : base(unitOfWork) {
            _page = page;
        }

        protected override ICollection<Course> Action()
        {
            return _unitOfWork.Courses.GetPartial(_page, 20);
        }
    }
}