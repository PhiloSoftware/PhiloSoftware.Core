using System;
using System.Security.Cryptography;

namespace PhiloSoftware.Core.Infrastructure {

    public interface ISequentialGuidGeneratorService
    {
        EnumSequentialGuidType GuidType { get; }

        Guid NewSequentialGuid();
    }

    /// <summary>
    /// The type of Guid to create.
    /// Different database's represent Guids differently and thus creating them wrong will perform poorly
    /// for insertion updating indexes.
    /// 
    /// <see cref="http://www.codeproject.com/Articles/388157/GUIDs-as-fast-primary-keys-under-multiple-database"/>
    /// Database                GUID Column 	    SequentialGuidType Value 
    /// Microsoft SQL Server    uniqueidentifier 	SequentialAtEnd
    /// MySQL 	                char(36)     	    SequentialAsString 
    /// Oracle 	                raw(16)         	SequentialAsBinary 
    /// PostgreSQL          	uuid    	        SequentialAsString
    /// </summary>
    public enum EnumSequentialGuidType
    {
        /// <summary>
        /// Use for MySQL & PostgreSQL
        /// </summary>
        SequentialAsString,

        /// <summary>
        /// Use for Oracle
        /// </summary>
        SequentialAsBinary,

        /// <summary>
        /// Use this if SQL Server
        /// </summary>
        SequentialAtEnd
    }

    public class SequentialGuidGenerator : ISequentialGuidGeneratorService
    {
        private readonly RNGCryptoServiceProvider _rng = new RNGCryptoServiceProvider();

        public SequentialGuidGenerator(EnumSequentialGuidType guidType)
        {
            GuidType = guidType;
        }

        public EnumSequentialGuidType GuidType
        {
            get;
            private set;
        }

        public Guid NewSequentialGuid()
        {
            byte[] randomBytes = new byte[10];
            _rng.GetBytes(randomBytes);

            long timestamp = DateTime.UtcNow.Ticks / 10000L;
            byte[] timestampBytes = BitConverter.GetBytes(timestamp);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
            }

            byte[] guidBytes = new byte[16];

            switch (GuidType)
            {
                case EnumSequentialGuidType.SequentialAsString:
                case EnumSequentialGuidType.SequentialAsBinary:
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

                    // If formatting as a string, we have to reverse the order
                    // of the Data1 and Data2 blocks on little-endian systems.
                    if (GuidType == EnumSequentialGuidType.SequentialAsString && BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(guidBytes, 0, 4);
                        Array.Reverse(guidBytes, 4, 2);
                    }
                    break;

                case EnumSequentialGuidType.SequentialAtEnd:
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 10);
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);
                    break;
            }

            return new Guid(guidBytes);
        }
    }
}