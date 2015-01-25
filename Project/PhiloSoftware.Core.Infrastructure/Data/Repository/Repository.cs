using PhiloSoftware.Core.Infrastructure.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhiloSoftware.Core.Infrastructure.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        IUnitOfWork _unitOfWork;
        IDataSource<T> _dataSource;

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dataSource = _unitOfWork.GetDataSource<T>();
        }

        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }

        public T GetByIDOrDefault(Guid id)
        {
            return _dataSource.SingleOrDefault(x => x.ID == id);
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

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _dataSource.GetEnumerator();
        }

        public Type ElementType
        {
            get { return _dataSource.AsQueryable().ElementType; }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return _dataSource.AsQueryable().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return _dataSource.AsQueryable().Provider; }
        }

        public Guid GetNewID()
        {
            return _unitOfWork.GetUniqueID();
        }
    }
}
