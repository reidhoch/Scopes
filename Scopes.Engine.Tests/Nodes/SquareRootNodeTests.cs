namespace Scopes.Engine.Tests.Nodes
{
    using System;

    using NUnit.Framework;

    using Scopes.Engine.Nodes;

    [TestFixture]
    public class SquareRootNodeTests
    {
        [Test]
        public void GetArity([Random(5)]double child)
        {
            var node = new SquareRootNode { Children = { new ConstantNode { Value = child } } };

            Assert.That(node.Arity, Is.EqualTo(1));
        }

        [Test]
        public void Clone([Values(1.0, 4.0, 9.0, 16.0, 25.0)] double childVal)
        {
            var node = new SquareRootNode { Children = { new ConstantNode { Value = childVal } } };
            Assert.That(node.Clone(), Is.Not.Null);
        }

        [Test]
        public void Equals([Values(1.0, 4.0, 9.0, 16.0, 25.0)] double childVal)
        {
            var left = new SquareRootNode { Children = { new ConstantNode { Value = childVal } } };
            var right = new SquareRootNode { Children = { new ConstantNode { Value = childVal } } };
            Assert.That(left.Equals(right), Is.True);
        }

        [Test]
        public void EqualsObject([Values(1.0, 4.0, 9.0, 16.0, 25.0)] double childVal)
        {
            var left = new SquareRootNode { Children = { new ConstantNode { Value = childVal } } };
            var right = new SquareRootNode { Children = { new ConstantNode { Value = childVal } } };
            Assert.That(left.Equals((object)right), Is.True);
        }

        [Test]
        public void EqualsNullIsFalse([Values(1.0, 4.0, 9.0, 16.0, 25.0)] double childVal)
        {
            var left = new SquareRootNode { Children = { new ConstantNode { Value = childVal } } };
            Assert.That(left.Equals(null), Is.False);
        }

        [Test]
        public void DifferentTypesAreNotEqual([Values(1.0, 4.0, 9.0, 16.0, 25.0)] double childVal)
        {
            var left = new SquareRootNode { Children = { new ConstantNode { Value = childVal } } };
            var right = new SubtractionNode
            {
                Children =
                                   {
                                       new ConstantNode { Value = childVal },
                                       new ConstantNode { Value = 2.0d * childVal }
                                   }
            };
            Assert.That(left.Equals(right), Is.False);
        }

        [Test]
        public void EqualsNullObjectIsFalse([Values(1.0, 4.0, 9.0, 16.0, 25.0)] double childVal)
        {
            var left = new SquareRootNode { Children = { new ConstantNode { Value = childVal } } };
            Assert.That(left.Equals((object)null), Is.False);
        }

        [Test]
        public void Evaluate([Values(1.0, 4.0, 9.0, 16.0, 25.0)] double childVal)
        {
            var expected = Math.Sqrt(childVal);
            var child = new ConstantNode { Value = childVal };
            var node = new SquareRootNode { Children = { child } };

            Assert.That(node.Evaluate(new double[0]), Is.EqualTo(expected));
        }

        [Test]
        public void GetHashCode([Values(1.0, 4.0, 9.0, 16.0, 25.0)] double childVal)
        {
            var node = new SquareRootNode { Children = { new ConstantNode { Value = childVal } } };
            var expected = node.Children.GetHashCode();
            Assert.That(node.GetHashCode(), Is.EqualTo(expected));
        }
    }
}
