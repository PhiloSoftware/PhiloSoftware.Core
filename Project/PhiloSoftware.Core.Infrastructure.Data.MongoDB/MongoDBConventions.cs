using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhiloSoftware.Core.Infrastructure.Data.MongoDB
{
    public static class MongoDBConventions
    {
        public static void NoIdMappingConvention<T>()
        {
            var noIdConventions = new ConventionProfile();
            noIdConventions.SetIdMemberConvention(new NamedIdMemberConvention("ID")); // no names
            ConventionRegistry.Register("noIdConvention", noIdConventions, t => t == typeof(T));
        }
    }
}
