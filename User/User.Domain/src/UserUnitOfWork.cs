using Core.Domain;
using User.Domain.UserAggregate.Repository;

namespace User.Domain
{
    public abstract class UserUnitOfWork : UnitOfWork
    {
        public abstract IUserRepository GetUserRepository();
    }
}