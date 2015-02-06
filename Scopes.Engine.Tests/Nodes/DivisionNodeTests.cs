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
        public void Clone([Random(5)]double left, [Random(5)]double right)
        {
            var node = new DivisionNode
            {
                Children =
                                   {
                                       new ConstantNode { Value = left },
                                       new ConstantNode { Value = right }
                                   }
            };
            Assert.That(node.Clone(), Is.Not.Null);
        }

        [Test]
        public void Equals([Random(5)]double leftVal, [Random(5)]double rightVal)
        {
            var left = new DivisionNode
            {
                Children =
                                   {
                                       new ConstantNode { Value = leftVal },
                                       new ConstantNode { Value = rightVal }
                                   }
            };
            var right = new DivisionNode
            {
                Children =
                                   {
                                       new ConstantNode { Value = leftVal },
                                       new ConstantNode { Value = rightVal }
                                   }
            };
            Assert.That(left.Equals(right), Is.True);
        }

        [Test]
        public void EqualsObject([Random(5)]double leftVal, [Random(5)]double rightVal)
        {
            var left = new DivisionNode
            {
                Children =
                                   {
                                       new ConstantNode { Value = leftVal },
                                       new ConstantNode { Value = rightVal }
                                   }
            };
            var right = new DivisionNode
            {
                Children =
                                   {
                                       new ConstantNode { Value = leftVal },
                                       new ConstantNode { Value = rightVal }
                                   }
            };
            Assert.That(left.Equals((object)right), Is.True);
        }

        [Test]
        public void EqualsNullIsFalse([Random(5)]double leftVal, [Random(5)]double rightVal)
        {
            var left = new DivisionNode
            {
                Children =
                                   {
                                       new ConstantNode { Value = leftVal },
                                       new ConstantNode { Value = rightVal }
                                   }
            };
            Assert.That(left.Equals(null), Is.False);
        }

        [Test]
        public void DifferentTypesAreNotEqual([Random(5)]double leftVal, [Random(5)]double rightVal)
        {
            var left = new DivisionNode
            {
                Children =
                                   {
                                       new ConstantNode { Value = leftVal },
                                       new ConstantNode { Value = rightVal }
                                   }
            };
            var right = new AdditionNode
            {
                Children =
                                   {
                                       new ConstantNode { Value = leftVal },
                                       new ConstantNode { Value = rightVal }
                                   }
            };
            Assert.That(left.Equals(right), Is.False);
        }

        [Test]
        public void EqualsNullObjectIsFalse([Random(5)]double leftVal, [Random(5)]double rightVal)
        {
            var left = new DivisionNode
            {
                Children =
                                   {
                                       new ConstantNode { Value = leftVal },
                                       new ConstantNode { Value = rightVal }
                                   }
            };
            Assert.That(left.Equals((object)null), Is.False);
        }

        [Test]
        public void Evaluate([Random(1.0, 10.0, 5)]double leftVal, [Random(1.0, 10.0, 5)]double rightVal)
        {
            var expected = leftVal / rightVal;
            var left = new ConstantNode { Value = leftVal };
            var right = new ConstantNode { Value = rightVal };
            var node = new DivisionNode { Children = { left, right } };

            Assert.That(node.Evaluate(new double[0]), Is.EqualTo(expected));
        }

        [Test]
        public void EvaluateZero([Random(1.0, 10.0, 5)]double leftVal)
        {
            const double Expected = Double.NaN;
            var left = new ConstantNode { Value = leftVal };
            var right = new ConstantNode { Value = 0.0d };
            var node = new DivisionNode { Children = { left, right } };
            
            Assert.That(node.Evaluate(new double[0]), Is.EqualTo(Expected));
        }

        [Test]
        public void GetHashCode([Random(5)] double leftVal, [Random(5)] double rightVal)
        {
            var node = new DivisionNode
            {
                Children =
                                   {
                                       new ConstantNode { Value = leftVal },
                                       new ConstantNode { Value = rightVal }
                                   }
            };
            var expected = node.Children.GetHashCode();
            Assert.That(node.GetHashCode(), Is.EqualTo(expected));
        }
    }
}
