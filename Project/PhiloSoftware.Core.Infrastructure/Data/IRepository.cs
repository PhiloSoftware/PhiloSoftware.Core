using PhiloSoftware.Core.Infrastructure.Implementation;
using System;

namespace PhiloSoftware.Core.Infrastructure.Data
{
    public interface IRepository<T> : IDataSource<T> where T : Entity
    {
        IUnitOfWork UnitOfWork { get; }
        T GetByIDOrDefault(Guid id);
        Guid GetNewID();
    }
}
