using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhiloSoftware.Core.Infrastructure.IOC
{
    public interface IDependencyScope : IDisposable
    {
        /// <summary>
        /// Get a registered object of type <see cref="typeparam"/> within this scope.
        /// </summary>
        /// <typeparam name="T">The type of object to get.</typeparam>
        /// <returns>A single registered object within this scope of type <see cref="typeparam"/>.</returns>
        T Get<T>();

        /// <summary>
        /// Gets all registered objects of type <see cref="typeparam"/> within this scope.
        /// </summary>
        /// <typeparam name="T">The type of the objects to get.</typeparam>
        /// <returns>All registered objects within this scope of type <see cref="typeparam"/>.</returns>
        IEnumerable<T> GetMany<T>();
    }
}
