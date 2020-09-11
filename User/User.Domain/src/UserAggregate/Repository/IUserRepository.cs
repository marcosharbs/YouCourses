using Core.Domain;

namespace User.Domain.UserAggregate.Repository
{
    public interface IUserRepository : IRepository<User.Domain.UserAggregate.Model.User>
    {
        
    }
}