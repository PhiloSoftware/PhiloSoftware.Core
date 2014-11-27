using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PhiloSoftware.Core.Infrastructure.Security
{
    public interface ICryptoProvider
    {
        byte[] GetNonZeroBytes(int length);
    }

    public class CryptoProvider : ICryptoProvider
    {
        public virtual byte[] GetNonZeroBytes(int length)
        {
            var bytes = new byte[length];
            var rng = new RNGCryptoServiceProvider();

            rng.GetNonZeroBytes(bytes);

            return bytes;
        }
    }
}
