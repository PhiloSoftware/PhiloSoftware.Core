using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhiloSoftware.Core.Infrastructure.Data.MongoDB
{
    public class MongoDBUnitOfWork : IUnitOfWork
    {
        private IProvideAConnectionString _connectionStringProvider;
        private IGenerateSequentialGuids _sequentialGuidGenerator;

        public MongoDBUnitOfWork(IProvideAConnectionString connectionStringProvider, IGenerateSequentialGuids sequentialGuidGenerator)
        {
            _connectionStringProvider = connectionStringProvider;
            _sequentialGuidGenerator = sequentialGuidGenerator;
        }

        public IDataSource<T> GetDataSource<T>() where T : IEntity
        {
            return new MongoDBDataSource<T>(_connectionStringProvider);
        }

        public void Commit()
        {
            // MongoDB is not transactional at this stage
        }

        public void RollBack()
        {
            // MongoDB is not transactional at this stage
        }

        public Guid GetUniqueID()
        {
            return _sequentialGuidGenerator.NewSequentialGuid();
        }
    }
}
