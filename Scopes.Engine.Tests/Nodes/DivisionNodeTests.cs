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
            var node = new DivisionNode
                           {
                               Children =
                                   {
                                       new ConstantNode { Value = left },
                                       new ConstantNode { Value = right }
                                   }
                           };

            Assert.That(node.Arity, Is.EqualTo(2));
        }

        [Test]
        public void Evaluate([Random(1.0, 10.0, 5)]double leftVal, [Random(1.0, 10.0, 5)]double rightVal)
        {
            var expected = leftVal / rightVal;
            var left = new ConstantNode { Value = leftVal };
            var right = new ConstantNode { Value = rightVal };
            var node = new DivisionNode { Children = { left, right } };

            Assert.That(node.Evaluate(), Is.EqualTo(expected));
        }

        [Test]
        public void EvaluateZero([Random(1.0, 10.0, 5)]double leftVal)
        {
            const double Expected = Double.NaN;
            var left = new ConstantNode { Value = leftVal };
            var right = new ConstantNode { Value = 0.0d };
            var node = new DivisionNode { Children = { left, right } };
            
            Assert.That(node.Evaluate(), Is.EqualTo(Expected));
        }
    }
}
