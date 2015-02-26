using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PhiloSoftware.Core.Infrastructure.Security
{
    /// <summary>
    ///     Supported hashing algorithms
    /// </summary>
    /// <remarks>
    ///     The values assigned can NOT be changed as this will make all
    ///     existing passwords not work anymore.
    /// </remarks>
    public enum SupportedHashAlgorithm
    {
        MD5 = 0,
        SHA1 = 1,
        SHA256 = 2,
        SHA384 = 3,
        SHA512 = 4,
        PBKDF2 = 5
    }

    /// <summary>
    ///     Class to hash a string and manage its representation as a hash.
    ///     If it is created using a hash string, it is laxy loaded to speed up data access.
    ///     The plain text string should NOT be stored in this object, once evaluated to a hash
    ///     it should not be able to be retrieved, the best you can do is compare some plain
    ///     text to see if it equalls this hashed value.
    /// </summary>
    public class HashedValue : IHashedValue
    {
        public const int SALT_BYTE_LENGTH = 8;

        /// <summary>
        /// Gets the identifier on the back of the hash which indicates the hashing algorithm. 
        /// This CANNOT change as it will invalidate existing hashes.
        /// </summary>
        private static readonly Func<SupportedHashAlgorithm, string> GetAlgoIdentifier =
            o => string.Format("algo={0}", Convert.ChangeType(o, o.GetTypeCode()));

        private SupportedHashAlgorithm _algorithm;
        private int _complexityThrottle;
        private bool _inited;
        private byte[] _saltBytes;
        private readonly ICryptoProvider _saltProvider;

        /// <summary>
        ///     Create a hashed representation of the plain text.
        /// </summary>
        /// <param name="plainText">The text to hash</param>
        /// <param name="supportedHashAlgorithm">If supplied the algorithm to hash the plain text with. Otherwise default.</param>
        /// <param name="complexityThrottle">
        ///     Offers the ability to tune the algorithm in order to increase the complexity. 1 being
        ///     the lowest value and 100 being very large.
        /// </param>
        /// <param name="saltProvider">Provider of the salt used for creating the hash.</param>
        private HashedValue(string plainText, SupportedHashAlgorithm supportedHashAlgorithm, int complexityThrottle, ICryptoProvider saltProvider)
        {
            _algorithm = supportedHashAlgorithm;
            _complexityThrottle = complexityThrottle;
            _saltProvider = saltProvider;
            ComputeHashWorker(plainText, supportedHashAlgorithm, complexityThrottle);
        }

        /// <summary>
        ///     Creates a hashed representation of the plain text.
        ///     This constructor uses the salt from the <see cref="oldHashedValue" /> in order to make comparison between
        ///     hashed values easier. This should be used with caution and is only recommended in instances such as
        ///     keeping a password history where you want to see if a password has been used before. If the hash has to
        ///     be calculated for each password in the history this could take a large amount of time.
        /// </summary>
        /// <param name="plainText">The text to hash</param>
        /// <param name="oldHashedValue">A hashed value in which to re-use the salt from.</param>
        /// <param name="saltProvider">Provider of the salt used for creating the hash.</param>
        private HashedValue(string plainText, HashedValue oldHashedValue)
        {
            _algorithm = oldHashedValue.Algorithm;
            _complexityThrottle = oldHashedValue.ComplexityThrottle;
            ComputeHashWorker(plainText, _algorithm, _complexityThrottle, oldHashedValue.SaltBytes);
        }

        private HashedValue(string hash)
        {
            HashString = hash;
        }

        /// <summary>
        ///     The hashed string representation
        /// </summary>
        public string HashString { get; protected set; }

        public virtual SupportedHashAlgorithm Algorithm
        {
            get
            {
                if (!_inited) Init();
                return _algorithm;
            }
        }

        public virtual int ComplexityThrottle
        {
            get
            {
                if (!_inited) Init();
                return _complexityThrottle;
            }
        }

        protected virtual byte[] SaltBytes
        {
            get
            {
                if (!_inited) Init();
                return _saltBytes;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((HashedValue)obj);
        }

        public bool Equals(string plainText)
        {
            return Equals(new HashedValue(plainText, this));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return HashString.GetHashCode();
            }
        }

        public override string ToString()
        {
            return HashString;
        }

        /// <summary>
        ///     Checks if the string value is hashed in a collection of values.
        ///     <remarks>
        ///         Because the hashed values can be hashed in various ways, the value param
        ///         has to be hashed for each value. This can be slow but optimisations in this method
        ///         speeds this up.
        ///     </remarks>
        /// </summary>
        /// <param name="plaintext">The plain text value to check.</param>
        /// <param name="values">The collection of values to check in.</param>
        /// <returns></returns>
        public static bool IsInCollection(string plaintext, IEnumerable<HashedValue> values, ICryptoProvider saltProvider)
        {
            // keep track of the hashed representations of value in the various methods.
            var hashedValues = new List<HashedValue>();

            return values.Any(
                ph =>
                {
                    var alreadyCalcd =
                        hashedValues.FirstOrDefault(
                            val => val.CanReuseSalt(ph));

                    if (alreadyCalcd != null)
                    {
                        return alreadyCalcd.HashString == ph.HashString;
                    }

                    var calc = new HashedValue(plaintext, ph);
                    hashedValues.Add(calc);

                    return calc.HashString == ph.HashString;
                });
        }

        private bool CanReuseSalt(HashedValue value)
        {
            return Algorithm == value.Algorithm && ComplexityThrottle == value.ComplexityThrottle && this.SaltBytes == value._saltBytes;
        }

        private bool Equals(HashedValue other)
        {
            return HashString.Equals(other.HashString);
        }

        private void Init()
        {
            if (_inited) return;

            _algorithm = DetermineHashTypeFromHash(HashString);

            byte[] hashWithSaltBytes = Convert.FromBase64String(HashString);
            int hashSizeInBits;
            bool hasIterationsCount = false;

            switch (_algorithm)
            {
                case SupportedHashAlgorithm.SHA1:
                    hashSizeInBits = 160;
                    break;
                case SupportedHashAlgorithm.SHA256:
                    hashSizeInBits = 256;
                    break;
                case SupportedHashAlgorithm.SHA384:
                    hashSizeInBits = 384;
                    break;
                case SupportedHashAlgorithm.SHA512:
                    hashSizeInBits = 512;
                    break;
                case SupportedHashAlgorithm.PBKDF2:
                    hashSizeInBits = 192;
                    hasIterationsCount = true;
                    break;
                default:
                    hashSizeInBits = 128;
                    break;
            }

            var hashSizeInBytes = hashSizeInBits / 8;
            var iterationCountSizeInBytes = hasIterationsCount ? 4 : 0;
            var algoMarkerSizeInBytes = _algorithm != SupportedHashAlgorithm.MD5 ? 6 : 0;

            if (hashWithSaltBytes.Length < (hashSizeInBytes + iterationCountSizeInBytes))
                throw new FormatException("Hash value should have salt but doesnt.");

            _saltBytes =
                new byte[
                    hashWithSaltBytes.Length - (hashSizeInBytes + iterationCountSizeInBytes + algoMarkerSizeInBytes)];
            for (int i = 0; i < _saltBytes.Length; i++)
                _saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

            var iterationBytes = new byte[iterationCountSizeInBytes];
            for (int i = 0; i < iterationBytes.Length; i++)
                iterationBytes[i] = hashWithSaltBytes[hashSizeInBytes + _saltBytes.Length + i];

            _complexityThrottle = hasIterationsCount ? BitConverter.ToInt32(iterationBytes, 0) : 1;

            _inited = true;
        }

        private void ComputeHashWorker(string plainText, SupportedHashAlgorithm algorithm, int complexityThrottle = 1,
            byte[] saltBytes = null)
        {
            if (saltBytes == null)
                saltBytes = _saltProvider.GetNonZeroBytes(SALT_BYTE_LENGTH);

            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var algoBytes = Encoding.UTF8.GetBytes(GetAlgoIdentifier(algorithm));

            byte[] hashBytes;
            var hasComplexityThrottle = false;

            _complexityThrottle = complexityThrottle;

            switch (algorithm)
            {
                case SupportedHashAlgorithm.SHA1:
                    hashBytes = CryptoHashAlgorithmComputeBytes(plainTextBytes, saltBytes, new SHA1Managed());
                    break;
                case SupportedHashAlgorithm.SHA256:
                    hashBytes = CryptoHashAlgorithmComputeBytes(plainTextBytes, saltBytes, new SHA256Managed());
                    break;
                case SupportedHashAlgorithm.SHA384:
                    hashBytes = CryptoHashAlgorithmComputeBytes(plainTextBytes, saltBytes, new SHA384Managed());
                    break;
                case SupportedHashAlgorithm.SHA512:
                    hashBytes = CryptoHashAlgorithmComputeBytes(plainTextBytes, saltBytes, new SHA512Managed());
                    break;
                case SupportedHashAlgorithm.PBKDF2:
                    complexityThrottle = complexityThrottle * 1000;

                    hashBytes = new Rfc2898DeriveBytes(plainTextBytes, saltBytes, complexityThrottle).GetBytes(24);
                    hasComplexityThrottle = true;
                    break;
                default:
                    hashBytes = CryptoHashAlgorithmComputeBytes(plainTextBytes, saltBytes,
                        new MD5CryptoServiceProvider());
                    break;
            }

            _saltBytes = saltBytes;

            if (hasComplexityThrottle)
                saltBytes = saltBytes.Concat(BitConverter.GetBytes(_complexityThrottle)).ToArray();

            saltBytes = saltBytes.Concat(algoBytes).ToArray();

            // append the salt bytes to the end of the hash
            IEnumerable<byte> hashWithSaltBytes = hashBytes.Concat(saltBytes);

            HashString = Convert.ToBase64String(hashWithSaltBytes.ToArray());

            _inited = true;
        }

        private static byte[] CryptoHashAlgorithmComputeBytes(IEnumerable<byte> plainTextBytes,
            IEnumerable<byte> saltBytes, HashAlgorithm hashAlgo)
        {
            var plainTextWithSaltBytes = plainTextBytes.Concat(saltBytes);

            return hashAlgo.ComputeHash(plainTextWithSaltBytes.ToArray());
        }

        private static SupportedHashAlgorithm DetermineHashTypeFromHash(string hash)
        {
            var hashBytes = Convert.FromBase64String(hash);

            var algoIndex = hashBytes.Length - 6;
            var bytes = new byte[6];

            for (var i = 0; i < 6; i++)
            {
                bytes[i] = hashBytes[algoIndex + i];
            }

            string algoBytes = Encoding.UTF8.GetString(bytes);

            if (algoBytes == GetAlgoIdentifier(SupportedHashAlgorithm.PBKDF2))
            {
                return SupportedHashAlgorithm.PBKDF2;
            }
            if (algoBytes == GetAlgoIdentifier(SupportedHashAlgorithm.SHA1))
            {
                return SupportedHashAlgorithm.SHA1;
            }
            if (algoBytes == GetAlgoIdentifier(SupportedHashAlgorithm.SHA256))
            {
                return SupportedHashAlgorithm.SHA256;
            }
            if (algoBytes == GetAlgoIdentifier(SupportedHashAlgorithm.SHA384))
            {
                return SupportedHashAlgorithm.SHA384;
            }
            if (algoBytes == GetAlgoIdentifier(SupportedHashAlgorithm.SHA512))
            {
                return SupportedHashAlgorithm.SHA512;
            }

            throw new Exception.NotAHashedValueStringException("The hash string is not a valid format.");
        }

        public class HashedValueFactory
        {
            private readonly ICryptoProvider _cryptoProvider;

            public HashedValueFactory(ICryptoProvider cryptoProvider)
            {
                _cryptoProvider = cryptoProvider;
            }

            public IHashedValue CreateHash(string text, SupportedHashAlgorithm algorithm, int hashComplexity)
            {
                return new HashedValue(text, algorithm, hashComplexity, _cryptoProvider);
            }

            public IHashedValue FromHash(string hashString)
            {
                return new HashedValue(hashString);
            }
        }
    }
}
