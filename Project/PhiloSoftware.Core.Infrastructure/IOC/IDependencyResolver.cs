using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhiloSoftware.Core.Infrastructure.IOC
{
    public interface IDependencyResolver : IDependencyScope
    {
        IDependencyScope BeginScope();
    }
}
