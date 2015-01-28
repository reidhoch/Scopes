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
        public void Evaluate([Values(1.0, 4.0, 9.0,16.0,25.0)]double childVal)
        {
            var expected = Math.Sqrt(childVal);
            var child = new ConstantNode { Value = childVal };
            var node = new SquareRootNode { Children = { child } };

            Assert.That(node.Evaluate(new double[0]), Is.EqualTo(expected));
        }
    }
}
