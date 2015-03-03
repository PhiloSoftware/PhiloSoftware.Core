using PhiloSoftware.Core.Infrastructure.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhiloSoftware.Core.Infrastructure.Data
{
    public interface ISession : IDisposable
    {
        void Add(IEntity ent);
        void Delete(IEntity ent);
        void Update(IEntity ent);
        T GetByID<T>(Guid id) where T : IEntity;
        void DeleteByID<T>(Guid id) where T : IEntity;
        IQueryable<T> Query<T>() where T : IEntity;
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
