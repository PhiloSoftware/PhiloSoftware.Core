using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhiloSoftware.Core.Infrastructure.Data
{
    public interface IEntity
    {
        Guid ID { get; }

        DateTimeOffset CreatedDateUtc { get; }

        DateTimeOffset UpdatedDateUtc { get; set; }
    }
}
