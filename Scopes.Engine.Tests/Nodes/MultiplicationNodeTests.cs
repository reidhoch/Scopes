namespace Scopes.Engine.Tests.Nodes
{
    using System;

    using NUnit.Framework;

    using Scopes.Engine.Nodes;

    [TestFixture]
    // ReSharper disable ObjectCreationAsStatement
    public class MultiplicationNodeTests
    {
        [Test]
        public void Constructor()
        {
            Assert.DoesNotThrow(() => new MultiplicationNode(new ConstantNode(5.0), new ConstantNode(5.0)));
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNullLeft()
        {
            new MultiplicationNode(null, new ConstantNode(5.0));
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNullRight()
        {
            new MultiplicationNode(new ConstantNode(5.0), null);
        }

        [Test]
        public void GetArity()
        {
            var node = new MultiplicationNode(new ConstantNode(5.0), new ConstantNode(5.0));

            Assert.That(node.Arity, Is.EqualTo(2));
        }

        [Test]
        public void GetChildren()
        {
            var left = new ConstantNode(2.5);
            var right = new ConstantNode(3.5);
            var node = new MultiplicationNode(left, right);

            Assert.That(node.Left, Is.EqualTo(left));
            Assert.That(node.Right, Is.EqualTo(right));
        }

        [Test]
        public void Evaluate([Random(1.0, 10.0, 5)]double leftVal, [Random(1.0, 10.0, 5)]double rightVal)
        {
            var expected = leftVal * rightVal;
            var left = new ConstantNode(leftVal);
            var right = new ConstantNode(rightVal);
            var node = new MultiplicationNode(left, right);

            Assert.That(node.Evaluate(), Is.EqualTo(expected));
        }
    }
}
