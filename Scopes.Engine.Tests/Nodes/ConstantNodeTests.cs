namespace Scopes.Engine.Tests.Nodes
{
    using NUnit.Framework;

    using Scopes.Engine.Nodes;

    [TestFixture]
    // ReSharper disable ObjectCreationAsStatement
    public class ConstantNodeTests
    {
        [Test]
        public void Constructor([Random(1.0, 10.0, 5)]double value)
        {
            Assert.DoesNotThrow(() => new ConstantNode { Value = value });
        }

        [Test]
        public void GetArity([Random(1.0, 10.0, 5)]double value)
        {
            var node = new ConstantNode { Value = value };

            Assert.That(node.Arity, Is.EqualTo(0));
        }

        [Test]
        public void Evaluate([Random(1.0, 10.0, 5)]double value)
        {
            var node = new ConstantNode { Value = value };

            Assert.That(node.Evaluate(), Is.EqualTo(value));
        }
    }
}
