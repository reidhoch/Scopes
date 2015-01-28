namespace Scopes.Engine.Tests.Nodes
{
    using NUnit.Framework;

    using Scopes.Engine.Nodes;

    [TestFixture]
    public class VariableNodeTests
    {
        [Test]
        public void Constructor()
        {
            Assert.DoesNotThrow(() => new VariableNode(0));
        }

        [Test]
        public void GetArity()
        {
            var node = new VariableNode(0);

            Assert.That(node.Arity, Is.EqualTo(0));
        }

        [Test]
        public void Evaluate([Random(0.0, 10.0, 5)]double value)
        {
            var parameters = new[] { value };
            var node = new VariableNode(0);

            Assert.That(node.Evaluate(parameters), Is.EqualTo(value));
        }
    }
}
