using PhiloSoftware.Core.Infrastructure.Definitions;

namespace PhiloSoftware.Core.Infrastructure.Data
{
    public interface IUnitOfWork
    {
        IDataSource<T> GetDataSource<T>() where T : IEntity;
        void Commit();
        void RollBack();
    }
}
