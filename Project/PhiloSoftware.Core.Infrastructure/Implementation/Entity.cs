using PhiloSoftware.Core.Infrastructure.Definitions;
using System;

namespace PhiloSoftware.Core.Infrastructure.Implementation
{
    public abstract class Entity : IEntity
    {
        [Obsolete("Do not use, this is for ORM's and DOD's")]
        public Entity() { }

        public Entity(IGenerateSequentialGuids sequentialGuidService)
        {
            this.Id = sequentialGuidService.NewSequentialGuid();
            this.CreatedDateUtc = DateTimeOffset.UtcNow;
            this.UpdatedDateUtc = DateTimeOffset.UtcNow;
        }

        public virtual Guid Id { get; private set; }

        public virtual DateTimeOffset CreatedDateUtc { get; private set; }

        public virtual DateTimeOffset UpdatedDateUtc { get; set; }

        public override bool Equals(object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            Entity p = (Entity)obj;
            return (Id == p.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
