using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhiloSoftware.Core.Infrastructure.Definitions
{
    public interface IAmAPerson : IEntity
    {
        string FirstName { get; }
        string LastName { get; }
        string EmailAddress { get; }
    }
}
