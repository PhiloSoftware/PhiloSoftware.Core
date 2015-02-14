using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using PhiloSoftware.Core.Infrastructure.Data.Exception;
using PhiloSoftware.Core.Infrastructure.Definitions;

namespace PhiloSoftware.Core.Infrastructure.Data.MongoDB
{
    public class MongoDbDataSource<T> : IDataSource<T> where T : IEntity
    {
        private readonly MongoCollection<T> _collection;

        public MongoDbDataSource(MongoCollection<T> collection)
        {
            _collection = collection;
        }

        public T GetByID(Guid id)
        {
            var query = Query<T>.EQ(t => t.ID, id);
            return _collection.FindOne(query);
        }

        public void Add(T entity)
        {
            if (entity.ID == Guid.Empty)
                throw IEntityIDNotSetException.ExceptionForInsertFail<T>();

            _collection.Insert(entity);
        }

        public void Update(T entity)
        {
            if (entity.ID == Guid.Empty)
                throw IEntityIDNotSetException.ExceptionForUpdateFail<T>();

            entity.UpdatedDateUtc = DateTimeOffset.UtcNow;
            _collection.Save<T>(entity);
        }

        public void Delete(T entity)
        {
            if (entity.ID == Guid.Empty)
                throw IEntityIDNotSetException.ExceptionForDeleteFail<T>();

            DeleteByID(entity.ID);
        }

        public void DeleteByID(Guid id)
        {
            var query = Query<T>.EQ(t => t.ID, id);
            _collection.Remove(query);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _collection.AsQueryable<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Type ElementType
        {
            get { return typeof(T); }
        }

        public Expression Expression
        {
            get
            {
                return _collection.AsQueryable<T>().Expression;
            }
        }

        public IQueryProvider Provider
        {
            get { return _collection.AsQueryable<T>().Provider; }
        }
    }
}
