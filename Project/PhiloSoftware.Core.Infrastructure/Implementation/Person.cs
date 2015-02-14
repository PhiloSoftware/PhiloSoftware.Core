using System;
using PhiloSoftware.Core.Infrastructure.Definitions;

namespace PhiloSoftware.Core.Infrastructure.Implementation
{
    public class Person : Entity , IAmAPerson
    {
        public Person(IGenerateSequentialGuids guidGenerator) : base(guidGenerator) { }

        public string FirstName { get; protected set; }

        public string LastName { get; protected set; }

        public string EmailAddress { get; protected set; }

        public string PhoneNumber { get; set; }
    }
}
