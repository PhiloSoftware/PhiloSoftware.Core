using System;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;

namespace PhiloSoftware.Core.Infrastructure.Data.MongoDB
{
    public static class MongoDBConventions
    {
        public static void IEntityIdMappingConvention()
        {
            var noIdConventions = new ConventionProfile();
            noIdConventions.SetIdMemberConvention(new NamedIdMemberConvention("Id")); // no names
            noIdConventions.SetIgnoreExtraElementsConvention(new AlwaysIgnoreExtraElementsConvention());
            ConventionRegistry.Register("noIdConvention", noIdConventions, t => true);
        }
    }
}
