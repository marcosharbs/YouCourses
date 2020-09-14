using Core.Domain;

namespace User.Domain.UserAggregate.Repository
{
    public interface IUserRepository : IRepository<User.Domain.UserAggregate.Model.User>
    {
        User.Domain.UserAggregate.Model.User getUserByEmail(string email);
    }
}