namespace Scopes.Engine.Tests.Mutation
{
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;

    using Scopes.Engine.Mutation;
    using Scopes.Engine.Nodes;

    [TestFixture]
    public class SinglePointMutationTests
    {
        private static readonly ISet<Func<IFunctionNode>> FunctionSet = new HashSet<Func<IFunctionNode>>
                                  {
                                      () => new SquareRootNode(),
                                      () => new AdditionNode(),
                                      () => new SubtractionNode(),
                                      () => new MultiplicationNode(),
                                      () => new DivisionNode()
                                  };

        [Test]
        public void Mutate([Values(1.0, 2.0)]double a, [Values(1.0, 2.0)]double b, [Values(1.0, 2.0)]double c)
        {
            var aNode = new ConstantNode { Value = a };
            var bNode = new ConstantNode { Value = b };
            var cNode = new ConstantNode { Value = c };
            var add = new AdditionNode();
            var subtract = new SubtractionNode();
            var mutation = new SinglePointMutation();
            var nodes = new List<IGepNode> { add, subtract, aNode, bNode, cNode };
            var original = new Chromosome(2, 1, 2, FunctionSet, nodes) { LinkingFunction = new AdditionNode() };
            var mutant = mutation.Mutate(original);

            var difference = 0;
            for (var idx = 0; idx < 5; idx++) {
                if (!mutant.Nodes[idx].Equals(original.Nodes[idx])) {
                    difference++;
                }
            }

            Assert.That(difference, Is.EqualTo(1));
        }
    }
}
