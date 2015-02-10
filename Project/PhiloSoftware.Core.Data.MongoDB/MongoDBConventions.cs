using System;
using System.Linq;
using MongoDB.Bson.Serialization.Conventions;

namespace PhiloSoftware.Core.Infrastructure.Data.MongoDB
{
    public static class MongoDBConventions
    {
        public static void IEntityIdMappingConvention<T>()
        {
            var noIdConventions = new ConventionProfile();
            noIdConventions.SetIdMemberConvention(new NamedIdMemberConvention("ID")); // no names
            ConventionRegistry.Register("noIdConvention", noIdConventions, t => t == typeof(T));
        }

        public static void IEntityIdMappingConvention(params Type[] types)
        {
            var noIdConventions = new ConventionProfile();
            noIdConventions.SetIdMemberConvention(new NamedIdMemberConvention("ID")); // no names

            types.ToList().ForEach(type => ConventionRegistry.Register("noIdConvention", noIdConventions, t => t == type));
            
        }
    }
}
