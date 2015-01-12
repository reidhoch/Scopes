namespace Scopes.Engine.Tests
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    // ReSharper disable ObjectCreationAsStatement
    public class ChromosomeTests
    {
        [Test]
        public void Constructor()
        {
            Assert.DoesNotThrow(() => new Chromosome(10, 1));
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructorNegativeHeadLength()
        {
            new Chromosome(-1, 1);
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructorZeroNumGenes()
        {
            new Chromosome(10, 0);
        }
    }
}
