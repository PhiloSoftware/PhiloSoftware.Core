using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhiloSoftware.Core.Infrastructure.Definitions
{
    public interface IEntity
    {
        Guid ID { get; }

        DateTimeOffset CreatedDateUtc { get; }

        DateTimeOffset UpdatedDateUtc { get; set; }
    }
}
