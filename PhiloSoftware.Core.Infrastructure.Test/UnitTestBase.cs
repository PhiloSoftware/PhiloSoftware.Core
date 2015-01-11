using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ninject.MockingKernel;
using PhiloSoftware.Core.Infrastructure.Data;

namespace PhiloSoftware.Core.Infrastructure.Test
{
    [TestClass]
    public class UnitTestBase
    {
        private IKernel _kernal = null;

        protected IKernel Kernal
        {
            get
            {
                return _kernal;
            }
            private set
            {
                _kernal = value;
            }
        }

        [TestInitialize]
        public void SetUp()
        {
            Kernal = new MockingKernel();
            Kernal.Bind<IUnitOfWork>().To<InMemoryUnitOfWork>();
            Kernal.Bind<ISequentialGuidGeneratorService>().To<SequentialGuidGenerator>().WithConstructorArgument<EnumSequentialGuidType>(EnumSequentialGuidType.SequentialAtEnd);
        }

        [TestCleanup]
        public void CleanUp()
        {
            // TODO remove all bindings to ensure it is a fresh run
            _kernal = null;
        }
    }
}
