using Library.Domain.Common;

namespace Library.Application
{
    public class GetTotalCoursesUseCase : UseCase<int>
    {

        public GetTotalCoursesUseCase(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        protected override int Action()
        {
            return _unitOfWork.Courses.Count();
        }
    }
}