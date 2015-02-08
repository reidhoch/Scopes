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
        public void Clone([Random(1, 10, 5)]int value)
        {
            var node = new VariableNode(value);
            Assert.That(node.Clone(), Is.Not.Null);
        }

        [Test]
        public void SameReference([Random(1, 10, 5)]int value)
        {
            var node = new VariableNode(value);
            Assert.That(node.Equals(node), Is.True);
        }

        [Test]
        public void ObjectSameReference([Random(1, 10, 5)]int value)
        {
            var node = new VariableNode(value);
            Assert.That(node.Equals(node as object), Is.True);
        }

        [Test]
        public void Equals([Random(1, 10, 5)]int value)
        {
            var left = new VariableNode(value);
            var right = new VariableNode(value);
            Assert.That(left.Equals(right), Is.True);
        }

        [Test]
        public void EqualsObject([Random(1, 10, 5)]int value)
        {
            var left = new VariableNode(value);
            var right = new VariableNode(value);
            Assert.That(left.Equals((object)right), Is.True);
        }

        [Test]
        public void EqualsNullIsFalse([Random(1, 10, 5)]int value)
        {
            var left = new VariableNode(value);
            Assert.That(left.Equals(null), Is.False);
        }

        [Test]
        public void DifferentTypesAreNotEqual([Random(1, 10, 5)]int value)
        {
            var left = new VariableNode(value);
            var right = new SubtractionNode
            {
                Children =
                                   {
                                       new ConstantNode { Value = value },
                                       new ConstantNode { Value = 2.0d * value }
                                   }
            };
            Assert.That(left.Equals(right), Is.False);
        }

        [Test]
        public void EqualsNullObjectIsFalse([Random(1, 10, 5)]int value)
        {
            var left = new VariableNode(value);
            Assert.That(left.Equals((object)null), Is.False);
        }

        [Test]
        public void Evaluate([Random(0.0, 10.0, 5)]double value)
        {
            var parameters = new[] { value };
            var node = new VariableNode(0);

            Assert.That(node.Evaluate(parameters), Is.EqualTo(value));
        }

        [Test]
        public void GetHashCode([Random(1, 10, 5)]int index)
        {
            var node = new VariableNode(index);
            var expected = index.GetHashCode();
            Assert.That(node.GetHashCode(), Is.EqualTo(expected));
        }
    }
}
