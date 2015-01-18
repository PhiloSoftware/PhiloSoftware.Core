using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhiloSoftware.Core.Infrastructure.Data
{
    public interface IRepository<T> : IDataSource<T> where T : IEntity
    {
        IUnitOfWork UnitOfWork { get; }
        T GetByIDOrDefault(Guid id);
        Guid GetNewID();
    }
}
