using System;
using System.Collections.Generic;
using Library.Domain.AuthorAggregate.Model;
using Library.Domain.AuthorAggregate.Repository;
using NHibernate;

namespace Library.Data.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private ISession _session;

        public AuthorRepository(ISession session)
        {
            _session = session;
        }

        public void AddOrUpdate(Author entity)
        {
            _session.SaveOrUpdate(entity);
        }

        public ICollection<Author> GetAll()
        {
            return _session.CreateCriteria(typeof(Author)).List<Author>();
        }

        public Author GetById(Guid id)
        {
            return _session.Get<Author>(id);
        }

        public void Remove(Author entity)
        {
            _session.Delete(entity);
        }
    }
}