using MongoDB.Driver;
using PhiloSoftware.Core.Infrastructure.Definitions;

namespace PhiloSoftware.Core.Infrastructure.Data.MongoDB
{
    public class MongoDbUnitOfWork : IUnitOfWork
    {
        private readonly MongoDatabase _database;

        public MongoDbUnitOfWork(IProvideAConnectionString connectionStringProvider)
        {
            var mongoClient = new MongoClient(connectionStringProvider.GetConnectionString());
            _database = mongoClient.GetServer().GetDatabase(connectionStringProvider.GetDataBaseName());
        }

        public IDataSource<T> GetDataSource<T>() where T : IEntity
        {
            return new MongoDbDataSource<T>(_database.GetCollection<T>(typeof(T).Name));
        }

        public void Commit()
        {
            // MongoDB is not transactional at this stage
        }

        public void RollBack()
        {
            // MongoDB is not transactional at this stage
        }
    }
}
