using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhiloSoftware.Core.Infrastructure.Data
{
    /// <summary>
    /// Data source that is in memory.
    /// </summary>
    /// <typeparam name="T">The type stored in this data source.</typeparam>
    public class InMemoryDataSource<T> : IDataSource<T> where T : IEntity
    {
        private readonly Dictionary<Guid, T> _dataSource;

        public InMemoryDataSource() : this(new Dictionary<Guid, T>()) { }

        public InMemoryDataSource(Dictionary<Guid, T> dataSource)
        {
            _dataSource = dataSource;
        }

        public T GetByID(Guid id)
        {
            return _dataSource[id];
        }

        public void Add(T entity)
        {
            _dataSource.Add(entity.ID, entity);
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            DeleteByID(entity.ID);
        }

        public void DeleteByID(Guid id)
        {
            _dataSource.Remove(id);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _dataSource.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _dataSource.Values.GetEnumerator();
        }

        public Type ElementType
        {
            get { return _dataSource.Values.AsQueryable().ElementType; }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return _dataSource.Values.AsQueryable().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return _dataSource.Values.AsQueryable().Provider; }
        }
    }
}
