using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhiloSoftware.Core.Infrastructure.Data
{
    public class SqlDataSource<T> : IDataSource<T> where T : IEntity
    {
        private ISession _session;

        public SqlDataSource(ISession session)
        {
            _session = session;
        }

        public T GetByID(Guid id)
        {
            return _session.GetByID<T>(id);
        }

        public void Add(T entity)
        {
            _session.Add(entity);
        }

        public void Update(T entity)
        {
            _session.Update(entity);
        }

        public void Delete(T entity)
        {
            _session.Delete(entity);
        }

        public void DeleteByID(Guid id)
        {
            _session.DeleteByID<T>(id);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _session.Query<T>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Type ElementType
        {
            get { return typeof(T); }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return _session.Query<T>().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return _session.Query<T>().Provider; }
        }
    }
}
