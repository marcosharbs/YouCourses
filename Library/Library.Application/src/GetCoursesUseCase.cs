using Library.Domain;
using Library.Domain.CourseAggregate.Model;
using System.Collections.Generic;
using Core.Application;

namespace Library.Application
{
    public class GetCoursesUseCase : UseCase<ICollection<Course>, ILibraryUnitOfWork>
    {
        private readonly int _page;

        private readonly int _pageSize;

        public GetCoursesUseCase(int page, int pageSize, ILibraryUnitOfWork unitOfWork) : base(unitOfWork) {
            _page = page;
            _pageSize = pageSize;
        }

        protected override ICollection<Course> Action()
        {
            return _unitOfWork.Courses.GetPartial(_page, _pageSize);
        }
    }
}