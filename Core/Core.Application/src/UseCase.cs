using Core.Domain;

namespace Core.Application
{
    public abstract class UseCase<T, U> where U : IUnitOfWork
    {
        protected U _unitOfWork;

        public UseCase(U unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public T Execute()
        {
            T result;
            try
            {
                _unitOfWork.BeginUnit();
                result = Action();
                _unitOfWork.CommitUnit();
            }
            catch
            {
                _unitOfWork.RollbackUnit();
                throw;
            }
            return result;
        }

        protected abstract T Action();
    }
}