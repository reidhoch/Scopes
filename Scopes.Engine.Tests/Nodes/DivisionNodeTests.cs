namespace Scopes.Engine.Tests.Nodes
{
    using System;

    using NUnit.Framework;

    using Scopes.Engine.Nodes;

    [TestFixture]
    public class DivisionNodeTests
    {
        [Test]
        public void GetArity([Random(5)]double left, [Random(5)]double right)
        {
            var node = new DivisionNode { Left = new ConstantNode(left), Right = new ConstantNode(right) };

            Assert.That(node.Arity, Is.EqualTo(2));
        }

        [Test]
        public void GetChildren()
        {
            var left = new ConstantNode(2.5);
            var right = new ConstantNode(3.5);
            var node = new DivisionNode { Left = left, Right = right };

            Assert.That(node.Left, Is.EqualTo(left));
            Assert.That(node.Right, Is.EqualTo(right));
        }

        [Test]
        public void Evaluate([Random(1.0, 10.0, 5)]double leftVal, [Random(1.0, 10.0, 5)]double rightVal)
        {
            var expected = leftVal / rightVal;
            var left = new ConstantNode(leftVal);
            var right = new ConstantNode(rightVal);
            var node = new DivisionNode { Left = left, Right = right };

            Assert.That(node.Evaluate(), Is.EqualTo(expected));
        }

        [Test]
        public void EvaluateZero([Random(1.0, 10.0, 5)]double leftVal)
        {
            const double Expected = Double.NaN;
            var left = new ConstantNode(leftVal);
            var right = new ConstantNode(0.0d);
            var node = new DivisionNode { Left = left, Right = right };

            Assert.That(node.Evaluate(), Is.EqualTo(Expected));
        }
    }
}
