using Library.Domain.Common;
using Library.Domain.CourseAggregate.Model;
using System.Collections.Generic;

namespace Library.Application
{
    public class GetCoursesUseCase : UseCase<ICollection<Course>>
    {
        private readonly int _page;

        private readonly int _pageSize;

        public GetCoursesUseCase(int page, int pageSize, IUnitOfWork unitOfWork) : base(unitOfWork) {
            _page = page;
            _pageSize = pageSize;
        }

        protected override ICollection<Course> Action()
        {
            return _unitOfWork.Courses.GetPartial(_page, _pageSize);
        }
    }
}