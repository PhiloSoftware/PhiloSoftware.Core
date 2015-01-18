using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhiloSoftware.Core.Infrastructure.Data
{
    /// <summary>
    /// Unit of work for an in memory 
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

        public Guid GetUniqueID()
        {
            return Guid.NewGuid();
        }
    }
}
