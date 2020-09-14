using System;
using System.Collections.Generic;
using User.Domain.UserAggregate.Repository;
using NHibernate;

namespace User.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private ISession _session;

        public UserRepository(ISession session)
        {
            _session = session;
        }

        public void AddOrUpdate(User.Domain.UserAggregate.Model.User entity)
        {
            _session.SaveOrUpdate(entity);
        }

        public int Count()
        {
            return _session.QueryOver<User.Domain.UserAggregate.Model.User>().RowCount();
        }

        public ICollection<User.Domain.UserAggregate.Model.User> GetAll()
        {
            return _session.CreateCriteria(typeof(User.Domain.UserAggregate.Model.User)).List<User.Domain.UserAggregate.Model.User>();
        }

        public User.Domain.UserAggregate.Model.User GetById(Guid id)
        {
            return _session.Get<User.Domain.UserAggregate.Model.User>(id);
        }

        public ICollection<User.Domain.UserAggregate.Model.User> GetPartial(int page, int pageSize)
        {
            return _session.CreateCriteria(typeof(User.Domain.UserAggregate.Model.User))
                            .SetFirstResult(page * pageSize)
                            .SetMaxResults(pageSize)
                            .List<User.Domain.UserAggregate.Model.User>();
        }

        public Domain.UserAggregate.Model.User getUserByEmail(string email)
        {
            return _session.QueryOver<Domain.UserAggregate.Model.User>()
                            .Where(user => user.UserEmail.Email == email)
                            .SingleOrDefault();
                    
        }

        public void Remove(User.Domain.UserAggregate.Model.User entity)
        {
            _session.Delete(entity);
        }
    }
}