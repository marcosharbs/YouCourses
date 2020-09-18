using Core.Domain;

namespace Core.Application
{
    public abstract class UseCase<T, U> where U : UnitOfWork
    {
        protected U _unitOfWork;

        public UseCase(U unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public T Execute()
        {
            _unitOfWork.BeginUnit();
            T result = Action();
            _unitOfWork.CommitUnit();
            return result;
        }

        protected abstract T Action();
    }
}