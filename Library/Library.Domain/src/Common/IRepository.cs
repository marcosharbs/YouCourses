using System;
using System.Collections.Generic;

namespace Library.Domain.Common
{
    public interface IRepository<T> where T: AggregateRoot
    {
        T GetById(Guid id);
        void AddOrUpdate(T entity);
        void Remove(T entity);
        ICollection<T> GetAll();
        ICollection<T> GetPartial(int page, int pageSize);
        int Count();
    }
}