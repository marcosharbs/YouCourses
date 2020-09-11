using Core.Domain;
using User.Domain.UserAggregate.Repository;

namespace User.Domain
{
    public interface IUserUnitOfWork : IUnitOfWork
    {
        IUserRepository Users { get; }
    }
}