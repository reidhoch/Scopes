namespace Scopes.Engine.Tests.Nodes
{
    using NUnit.Framework;

    using Scopes.Engine.Nodes;

    [TestFixture]
    public class VariableNodeTests
    {
        [Test]
        public void Constructor([Random(1.0, 10.0, 5)]double value)
        {
            Assert.DoesNotThrow(() => new VariableNode { Value = value });
        }

        [Test]
        public void GetArity([Values("A", "B", "C", "D", "E")]string name, [Random(1.0, 10.0, 5)]double value)
        {
            var node = new VariableNode { Name = name, Value = value };

            Assert.That(node.Arity, Is.EqualTo(0));
        }

        [Test]
        public void Evaluate([Values("A", "B", "C", "D", "E")]string name, [Random(1.0, 10.0, 5)]double value)
        {
            var node = new VariableNode { Name = name, Value = value };

            Assert.That(node.Evaluate(), Is.EqualTo(value));
        }
    }
}
