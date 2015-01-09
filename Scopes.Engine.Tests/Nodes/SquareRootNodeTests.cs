namespace Scopes.Engine.Tests.Nodes
{
    using System;

    using NUnit.Framework;

    using Scopes.Engine.Nodes;

    [TestFixture]
    // ReSharper disable ObjectCreationAsStatement
    public class SquareRootNodeTests
    {
        [Test]
        public void Constructor()
        {
            Assert.DoesNotThrow(() => new SquareRootNode(new ConstantNode(25.0)));
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNullChild()
        {
            new SquareRootNode(null);
        }

        [Test]
        public void GetArity()
        {
            var node = new SquareRootNode(new ConstantNode(25.0));

            Assert.That(node.Arity, Is.EqualTo(1));
        }

        [Test]
        public void GetChild()
        {
            var child = new ConstantNode(25.0);
            var node = new SquareRootNode(child);

            Assert.That(node.Child, Is.EqualTo(child));
        }

        [Test]
        public void Evaluate([Values(1.0, 4.0, 9.0,16.0,25.0)]double childVal)
        {
            var expected = Math.Sqrt(childVal);
            var child = new ConstantNode(childVal);
            var node = new SquareRootNode(child);

            Assert.That(node.Evaluate(), Is.EqualTo(expected));
        }
    }
}
