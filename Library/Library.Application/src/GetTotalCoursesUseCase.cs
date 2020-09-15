using Library.Domain;
using Core.Application;

namespace Library.Application
{
    public class GetTotalCoursesUseCase : UseCase<int, LibraryUnitOfWork>
    {

        public GetTotalCoursesUseCase(LibraryUnitOfWork unitOfWork) : base(unitOfWork) { }

        protected override int Action()
        {
            return _unitOfWork.GetCourseRepository().Count();
        }
    }
}