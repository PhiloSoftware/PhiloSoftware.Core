using PhiloSoftware.Core.Infrastructure.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhiloSoftware.Core.Infrastructure.Implementation
{
    public class Person : Entity , IAmAPerson
    {
        [Obsolete("Do not use, this is for ORM's and DOD's")]
        public Person() {}
        public Person(IGenerateSequentialGuids guidGenerator) : base(guidGenerator) { }

        public string FirstName { get; protected set; }

        public string LastName { get; protected set; }

        public string EmailAddress { get; protected set; }

        public string PhoneNumber { get; set; }
    }
}
