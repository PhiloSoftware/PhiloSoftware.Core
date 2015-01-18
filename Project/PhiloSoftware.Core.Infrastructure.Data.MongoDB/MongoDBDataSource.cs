using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using PhiloSoftware.Core.Infrastructure.Data.Exception;

namespace PhiloSoftware.Core.Infrastructure.Data.MongoDB
{
    public class MongoDBDataSource<T> : IDataSource<T> where T : IEntity
    {
        private MongoClient _mongoClient;
        private MongoDatabase _database;
        private MongoCollection<T> _collection;

        public MongoDBDataSource(IProvideAConnectionString connectionProvider)
        {
            _mongoClient = new MongoClient(connectionProvider.GetConnectionString());
            _database = _mongoClient.GetServer().GetDatabase(connectionProvider.GetDataBaseName());
            _collection = _database.GetCollection<T>(typeof(T).Name);
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
