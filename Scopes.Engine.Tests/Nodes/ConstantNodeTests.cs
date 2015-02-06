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
        public void Clone([Random(1.0, 10.0, 5)]double value)
        {
            var node = new ConstantNode { Value = value };
            Assert.That(node.Clone(), Is.Not.Null);
        }

        [Test]
        public void Equals([Random(1.0, 10.0, 5)]double value)
        {
            var left = new ConstantNode { Value = value };
            var right = new ConstantNode { Value = value };
            Assert.That(left.Equals(right), Is.True);
        }

        [Test]
        public void EqualsObject([Random(1.0, 10.0, 5)]double value)
        {
            var left = new ConstantNode { Value = value };
            var right = new ConstantNode { Value = value };
            Assert.That(left.Equals((object)right), Is.True);
        }

        [Test]
        public void EqualsNullIsFalse([Random(1.0, 10.0, 5)]double value)
        {
            var left = new ConstantNode { Value = value };
            Assert.That(left.Equals(null), Is.False);
        }

        [Test]
        public void DifferentTypesAreNotEqual([Random(1.0, 10.0, 5)]double value)
        {
            var left = new ConstantNode { Value = value };
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
        public void EqualsNullObjectIsFalse([Random(1.0, 10.0, 5)]double value)
        {
            var left = new ConstantNode { Value = value };
            Assert.That(left.Equals((object)null), Is.False);
        }

        [Test]
        public void Evaluate([Random(1.0, 10.0, 5)]double value)
        {
            var node = new ConstantNode { Value = value };

            Assert.That(node.Evaluate(new double[0]), Is.EqualTo(value));
        }

        [Test]
        public void GetHashCode([Random(1.0, 10.0, 5)]double value)
        {
            var node = new ConstantNode { Value = value };
            var expected = value.GetHashCode();
            Assert.That(node.GetHashCode(), Is.EqualTo(expected));
        }
    }
}
