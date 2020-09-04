using Library.Domain.Common;
using System;

namespace Library.Application
{
    public abstract class UseCase<T>
    {
        protected IUnitOfWork _unitOfWork;

        public UseCase(IUnitOfWork unitOfWork)
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
            catch (Exception e)
            {
                _unitOfWork.RollbackUnit();
                throw e;
            }
            return result;
        }

        protected abstract T Action();
    }
}