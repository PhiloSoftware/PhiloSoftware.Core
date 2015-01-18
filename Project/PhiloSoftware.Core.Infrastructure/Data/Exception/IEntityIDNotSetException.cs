using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhiloSoftware.Core.Infrastructure.Data.Exception
{
    public class IEntityIDNotSetException : System.Exception
    {
        private IEntityIDNotSetException(string message) : base(message) { }

        public static IEntityIDNotSetException ExceptionForInsertFail<T>()
        {
            var message = string.Format("Cannot insert entity {0} as the ID has not been set.", typeof(T));
            return new IEntityIDNotSetException(message);
        }

        public static IEntityIDNotSetException ExceptionForUpdateFail<T>()
        {
            var message = string.Format("Cannot update entity {0} as the ID has not been set.", typeof(T));
            return new IEntityIDNotSetException(message);
        }

        public static System.Exception ExceptionForDeleteFail<T>()
        {
            var message = string.Format("Cannot delete entity {0} as the ID has not been set.", typeof(T));
            return new IEntityIDNotSetException(message);
        }
    }
}
