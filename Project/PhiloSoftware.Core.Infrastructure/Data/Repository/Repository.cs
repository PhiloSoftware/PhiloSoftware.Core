using PhiloSoftware.Core.Infrastructure.Implementation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhiloSoftware.Core.Infrastructure.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        readonly IDataSource<T> _dataSource;

        public Repository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            _dataSource = UnitOfWork.GetDataSource<T>();
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public T GetByIDOrDefault(Guid id)
        {
            return _dataSource.FirstOrDefault(x => x.Id == id);
        }

        public T GetByID(Guid id)
        {
            return _dataSource.GetByID(id);
        }

        public virtual void Add(T entity)
        {
            _dataSource.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _dataSource.Update(entity);
        }

        public virtual void Delete(T entity)
        {
            _dataSource.Delete(entity);
        }

        public virtual void DeleteByID(Guid id)
        {
            _dataSource.DeleteByID(id);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _dataSource.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dataSource.GetEnumerator();
        }

        public Type ElementType
        {
            get { return _dataSource.AsQueryable().ElementType; }
        }

        public Expression Expression
        {
            get { return _dataSource.AsQueryable().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return _dataSource.AsQueryable().Provider; }
        }
    }
}
