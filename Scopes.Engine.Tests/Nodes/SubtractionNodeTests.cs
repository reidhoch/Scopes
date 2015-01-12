namespace Scopes.Engine.Tests.Nodes
{
    using NUnit.Framework;

    using Scopes.Engine.Nodes;

    [TestFixture]
    public class SubtractionNodeTests
    {
        [Test]
        public void GetArity([Random(5)]double left, [Random(5)]double right)
        {
            var node = new SubtractionNode
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
            var expected = leftVal - rightVal;
            var left = new ConstantNode { Value = leftVal };
            var right = new ConstantNode { Value = rightVal };
            var node = new SubtractionNode { Children = { left, right } };

            Assert.That(node.Evaluate(), Is.EqualTo(expected));
        }
    }
}
