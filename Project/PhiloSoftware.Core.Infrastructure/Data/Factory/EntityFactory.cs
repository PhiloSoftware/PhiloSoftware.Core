using PhiloSoftware.Core.Infrastructure.Data.Repository;
using PhiloSoftware.Core.Infrastructure.Implementation;

namespace PhiloSoftware.Core.Infrastructure.Data.Factory
{
    public abstract class EntityFactory<T> where T : Entity
    {
        protected EntityFactory(Repository<T> repository, IGenerateSequentialGuids guidGenerator)
        {
            Repository = repository;
            GuidGenerator = guidGenerator;
        }

        public Repository<T> Repository { get; private set; }
        protected IGenerateSequentialGuids GuidGenerator { get; set; }
    }
}
