using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhiloSoftware.Core.Infrastructure.Security.Exception
{
    public class NotAHashedValueStringException : System.Exception
    {
        public NotAHashedValueStringException(string message)
            : base(message) { }
    }
}
