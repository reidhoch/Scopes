namespace Scopes.Engine.Tests
{
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;

    using Scopes.Engine.Nodes;

    [TestFixture]
    // ReSharper disable ObjectCreationAsStatement
    public class ChromosomeTests
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
        public void Constructor()
        {
            Assert.DoesNotThrow(() => new Chromosome(10, 1, FunctionSet));
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructorNegativeHeadLength()
        {
            new Chromosome(-1, 1, FunctionSet);
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructorZeroNumGenes()
        {
            new Chromosome(10, 0, FunctionSet);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNullFunctionSet()
        {
            new Chromosome(10, 1, null, new List<IGepNode> { new ConstantNode() });
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNullNodes()
        {
            new Chromosome(10, 1, FunctionSet, null);
        }

        [Test]
        public void ConstructorNodes()
        {
            var expected = new List<IGepNode> { new ConstantNode() };
            var chromosome = new Chromosome(10, 1, FunctionSet, expected);
            Assert.That(chromosome.Nodes, Is.EquivalentTo(expected));
        }

        [Test]
        public void GetFunctionSet()
        {
            var nodes = new List<IGepNode> { new ConstantNode() };
            var chromosome = new Chromosome(10, 1, FunctionSet, nodes);
            Assert.That(chromosome.FunctionSet, Is.EquivalentTo(FunctionSet));
        }

        [Test]
        public void GetHeadLength([Random(5, 10, 5)]int headLength)
        {
            var nodes = new List<IGepNode> { new ConstantNode() };
            var chromosome = new Chromosome(headLength, 1, FunctionSet, nodes);
            Assert.That(chromosome.HeadLength, Is.EqualTo(headLength));
        }

        [Test]
        public void GetNumGenes([Random(5, 10, 5)]int numGenes)
        {
            var nodes = new List<IGepNode> { new ConstantNode() };
            var chromosome = new Chromosome(10, numGenes, FunctionSet, nodes);
            Assert.That(chromosome.NumGenes, Is.EqualTo(numGenes));
        }

        [Test]
        public void GetTreeSingleTerminal([Random(0.0d, 1.0d, 5)]double value)
        {
            var expected = new ConstantNode { Value = value };
            var nodes = new List<IGepNode> { expected };
            var chromosome = new Chromosome(10, 1, FunctionSet, nodes);
            var actual = chromosome.Tree;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetTree([Random(0.0d, 1.0d, 5)]double a, [Random(0.0d, 1.0d, 5)]double b)
        {
            var aNode = new ConstantNode { Value = a };
            var bNode = new ConstantNode { Value = b };
            var root = new AdditionNode();
            var expected = new AdditionNode { Children = { aNode, bNode } };

            var nodes = new List<IGepNode> { root, aNode, bNode };
            var chromosome = new Chromosome(10, 1, FunctionSet, nodes);
            var actual = chromosome.Tree;

            Assert.That(actual.Evaluate(new double[0]), Is.EqualTo(expected.Evaluate(new double[0])).Within(0.0001));
        }

        [Test]
        public void GetTreeMultipleFunctions([Values(1.0, 2.0)]double a, [Values(1.0, 2.0)]double b, [Values(1.0, 2.0)]double c)
        {
            var aNode = new ConstantNode { Value = a };
            var bNode = new ConstantNode { Value = b };
            var cNode = new ConstantNode { Value = c };
            var add = new AdditionNode();
            var subtract = new SubtractionNode();
            var expected = new AdditionNode
                               {
                                   Children =
                                       {
                                           new SubtractionNode
                                               {
                                                   Children = { bNode, cNode }
                                               }, 
                                           aNode
                                       }
                               };

            var nodes = new List<IGepNode> { add, subtract, aNode, bNode, cNode };
            var chromosome = new Chromosome(2, 1, FunctionSet, nodes);
            var actual = chromosome.Tree;

            Assert.That(actual.Evaluate(new double[0]), Is.EqualTo(expected.Evaluate(new double[0])).Within(0.0001));
        }
    }
}
