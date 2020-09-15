using Library.Domain;
using Library.Domain.CourseAggregate.Model;
using System.Collections.Generic;
using Core.Application;

namespace Library.Application
{
    public class GetCoursesUseCase : UseCase<ICollection<Course>, LibraryUnitOfWork>
    {
        private readonly int _page;

        private readonly int _pageSize;

        public GetCoursesUseCase(int page, int pageSize, LibraryUnitOfWork unitOfWork) : base(unitOfWork) {
            _page = page;
            _pageSize = pageSize;
        }

        protected override ICollection<Course> Action()
        {
            return _unitOfWork.GetCourseRepository().GetPartial(_page, _pageSize);
        }
    }
}