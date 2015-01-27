using PhiloSoftware.Core.Infrastructure.Definitions;

namespace PhiloSoftware.Core.Infrastructure.Data.InMemory
{
    /// <summary>
    /// Unit of work for an in memory unit of work
    /// </summary>
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        public IDataSource<T> GetDataSource<T>() where T : IEntity
        {
            return new InMemoryDataSource<T>();
        }

        public void RegisterNew(IEntity @object)
        {

        }

        public void RegisterDirty(IEntity @object)
        {

        }

        public void RegisterClean(IEntity @object)
        {

        }

        public void RegisterDelete(IEntity @object)
        {

        }

        public void Commit()
        {

        }

        public void RollBack()
        {

        }
    }
}
