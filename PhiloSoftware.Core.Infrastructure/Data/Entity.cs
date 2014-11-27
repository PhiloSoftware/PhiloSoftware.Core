using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhiloSoftware.Core.Infrastructure.Data
{
    public class Entity : IEntity
    {
        public Entity(ISequentialGuidGeneratorService sequentialGuidService)
        {
            this.ID = sequentialGuidService.NewSequentialGuid();
        }

        public Guid ID { get; private set; }

        public DateTimeOffset CreatedDateUtc { get; set; }

        public DateTimeOffset UpdatedDateUtc { get; set; }

        public override bool Equals(object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            Entity p = (Entity)obj;
            return (ID == p.ID);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
}
