using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhiloSoftware.Core.Infrastructure.Security
{
    /// <summary>
    /// Class to hash a string and manage its representation as a hash.
    /// </summary>
    public interface IHashedValue
    {
        string HashString { get; }
        SupportedHashAlgorithm Algorithm { get; }
        int ComplexityThrottle { get; }

        /// <summary>
        /// Return true if this IHashedValue is a hashed representation of the plainText
        /// </summary>
        bool Equals(string plainText);
    }
}