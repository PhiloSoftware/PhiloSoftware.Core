using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhiloSoftware.Core.Infrastructure.Data
{
    public abstract class Entity : IEntity
    {
        [Obsolete("Do not use, this is for ORM's and DOD's")]
        public Entity() { }

        public Entity(IGenerateSequentialGuids sequentialGuidService)
        {
            this.ID = sequentialGuidService.NewSequentialGuid();
            this.CreatedDateUtc = DateTimeOffset.UtcNow;
            this.UpdatedDateUtc = DateTimeOffset.UtcNow;
        }

        public virtual Guid ID { get; private set; }

        public virtual DateTimeOffset CreatedDateUtc { get; private set; }

        public virtual DateTimeOffset UpdatedDateUtc { get; set; }

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
