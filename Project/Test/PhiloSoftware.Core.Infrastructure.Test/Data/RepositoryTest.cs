using System;
using System.Linq;
using Ninject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhiloSoftware.Core.Infrastructure.Data;

namespace PhiloSoftware.Core.Infrastructure.Test
{
    [TestClass]
    public class RepositoryTest : UnitTestBase
    {
        [TestMethod]
        public void RepositoryAdd()
        {
            var entityRepo = base.Kernal.Get<Repository<TestEntity>>();

            var testEnt = new TestEntity(base.Kernal.Get<ISequentialGuidGeneratorService>());

            entityRepo.Add(testEnt);

            var repoEnt = entityRepo.GetByID(testEnt.ID);

            Assert.AreEqual(testEnt, repoEnt);
        }

        [TestMethod]
        public void RepositoryDelete()
        {
            var entityRepo = base.Kernal.Get<Repository<TestEntity>>();

            var testEnt = new TestEntity(base.Kernal.Get<ISequentialGuidGeneratorService>());

            entityRepo.Add(testEnt);
            entityRepo.Delete(testEnt);

            Assert.IsNull(entityRepo.SingleOrDefault(ent => ent == testEnt));
        }

        public class TestEntity : Entity
        {
            public TestEntity(ISequentialGuidGeneratorService guidService) : base(guidService) { }
        }
    }
}
