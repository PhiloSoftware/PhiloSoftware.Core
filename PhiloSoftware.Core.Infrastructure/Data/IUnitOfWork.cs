using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhiloSoftware.Core.Infrastructure.Data
{
    public interface IUnitOfWork
    {
        IDataSource<T> GetDataSource<T>() where T : IEntity;

        void Commit();
        void RollBack();

        Guid GetUniqueID();
    }
}
