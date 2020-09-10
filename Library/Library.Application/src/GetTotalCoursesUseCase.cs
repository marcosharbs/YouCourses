using Library.Domain;

namespace Library.Application
{
    public class GetTotalCoursesUseCase : UseCase<int, ILibraryUnitOfWork>
    {

        public GetTotalCoursesUseCase(ILibraryUnitOfWork unitOfWork) : base(unitOfWork) { }

        protected override int Action()
        {
            return _unitOfWork.Courses.Count();
        }
    }
}