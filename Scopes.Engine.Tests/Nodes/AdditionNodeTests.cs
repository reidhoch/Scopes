namespace Scopes.Engine.Tests.Nodes
{
    using NUnit.Framework;
    using Scopes.Engine.Nodes;
    using System;

    [TestFixture]
    public class AdditionNodeTests
    {
        [Test]
        public void Constructor()
        {
            Assert.DoesNotThrow(() => new AdditionNode(new ConstantNode(5.0), new ConstantNode(5.0)));
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNullLeft()
        {
            new AdditionNode(null, new ConstantNode(5.0));
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNullRight()
        {
            new AdditionNode(new ConstantNode(5.0), null);
        }

        [Test]
        public void GetArity()
        {
            var node = new AdditionNode(new ConstantNode(5.0), new ConstantNode(5.0));

            Assert.That(node.Arity, Is.EqualTo(2));
        }

        [Test]
        public void GetChildren()
        {
            var left = new ConstantNode(2.5);
            var right = new ConstantNode(3.5);
            var node = new AdditionNode(left, right);

            Assert.That(node.Left, Is.EqualTo(left));
            Assert.That(node.Right, Is.EqualTo(right));
        }

        [Test]
        public void Evaluate()
        {
            var left = new ConstantNode(2.5);
            var right = new ConstantNode(3.5);
            var node = new AdditionNode(left, right);

            Assert.That(node.Evaluate(), Is.EqualTo(6.0));
        }
    }
}
