using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhiloSoftware.Core.Infrastructure.Data
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        private ISession _session;

        public SqlUnitOfWork(ISession session)
        {
            _session = session;
        }

        public IDataSource<T> GetDataSource<T>() where T : IEntity
        {
            return new SqlDataSource<T>(_session);
        }

        public void RegisterNew(IEntity @object)
        {
            throw new NotImplementedException();
        }

        public void RegisterDirty(IEntity @object)
        {
            throw new NotImplementedException();
        }

        public void RegisterClean(IEntity @object)
        {
            throw new NotImplementedException();
        }

        public void RegisterDelete(IEntity @object)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void RollBack()
        {
            throw new NotImplementedException();
        }

        public Guid GetUniqueID()
        {
            // query the db
            throw new NotImplementedException();
        }
    }
}
