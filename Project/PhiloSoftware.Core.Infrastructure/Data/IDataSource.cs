using PhiloSoftware.Core.Infrastructure.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhiloSoftware.Core.Infrastructure.Data
{
    public interface IDataSource<T> : IQueryable<T> where T : IEntity
    {
        T GetByID(Guid id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteByID(Guid id);
    }
}
