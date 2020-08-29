using System;
using System.Collections.Generic;
using Library.Domain.CourseAggregate.Model;
using Library.Domain.CourseAggregate.Repository;
using NHibernate;

namespace Library.Data.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private ISession _session;

        public CourseRepository(ISession session)
        {
            _session = session;
        }

        public void AddOrUpdate(Course entity)
        {
            _session.SaveOrUpdate(entity);
        }

        public ICollection<Course> GetAll()
        {
            return _session.CreateCriteria(typeof(Course)).List<Course>();
        }

        public Course GetById(Guid id)
        {
            return _session.Get<Course>(id);
        }

        public void Remove(Course entity)
        {
            _session.Delete(entity);
        }
    }
}