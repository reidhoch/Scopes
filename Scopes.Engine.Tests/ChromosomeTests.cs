namespace Scopes.Engine.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
            Assert.DoesNotThrow(() => new Chromosome(10, 1, 2, FunctionSet));
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructorNegativeHeadLength()
        {
            new Chromosome(-1, 1, 2, FunctionSet);
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructorZeroNumGenes()
        {
            new Chromosome(10, 0, 2, FunctionSet);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNullFunctionSet()
        {
            new Chromosome(10, 1, 2, null, new List<IGepNode> { new ConstantNode() });
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNullNodes()
        {
            new Chromosome(10, 1, 2, FunctionSet, null);
        }

        [Test]
        public void ConstructorNodes()
        {
            var expected = new List<IGepNode> { new ConstantNode() };
            var chromosome = new Chromosome(10, 1, 2, FunctionSet, expected);
            Assert.That(chromosome.Nodes, Is.EquivalentTo(expected));
        }

        [Test]
        public void GetFunctionSet()
        {
            var nodes = new List<IGepNode> { new ConstantNode() };
            var chromosome = new Chromosome(10, 1, 2, FunctionSet, nodes);
            Assert.That(chromosome.FunctionSet, Is.EquivalentTo(FunctionSet));
        }

        [Test]
        public void GetHeadLength([Random(5, 10, 5)]int headLength)
        {
            var nodes = new List<IGepNode> { new ConstantNode() };
            var chromosome = new Chromosome(headLength, 1, 2, FunctionSet, nodes);
            Assert.That(chromosome.HeadLength, Is.EqualTo(headLength));
        }

        [Test]
        public void GetTailLength([Random(5, 10, 5)]int headLength)
        {
            var nodes = new List<IGepNode> { new ConstantNode() };
            var chromosome = new Chromosome(headLength, 1, 2, FunctionSet, nodes);
            var maxArity = FunctionSet.Select(func => func().Arity).Concat(new[] { Int32.MinValue }).Max();
            Assert.That(chromosome.TailLength, Is.EqualTo((headLength * (maxArity - 1)) + 1));
        }

        [Test]
        public void GetLength([Random(5, 10, 5)]int headLength)
        {
            var nodes = new List<IGepNode> { new ConstantNode() };
            var chromosome = new Chromosome(headLength, 1, 2, FunctionSet, nodes);
            var maxArity = FunctionSet.Select(func => func().Arity).Concat(new[] { Int32.MinValue }).Max();
            Assert.That(chromosome.Length, Is.EqualTo(headLength + (headLength * (maxArity - 1)) + 1));
        }

        [Test]
        public void GetNumGenes([Random(5, 10, 5)]int numGenes)
        {
            var nodes = new List<IGepNode> { new ConstantNode() };
            var chromosome = new Chromosome(10, numGenes, 2, FunctionSet, nodes);
            Assert.That(chromosome.NumGenes, Is.EqualTo(numGenes));
        }

        [Test]
        public void GetTreeSingleTerminal([Random(0.0d, 1.0d, 5)]double value)
        {
            var expected = new ConstantNode { Value = value };
            var nodes = new List<IGepNode> { expected, expected, expected };
            var chromosome = new Chromosome(1, 1, 2, FunctionSet, nodes);
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
            var chromosome = new Chromosome(1, 1, 2, FunctionSet, nodes);
            var actual = chromosome.Tree;

            Assert.That(actual.Evaluate(new double[0]), Is.EqualTo(expected.Evaluate(new double[0])).Within(0.0001));
        }

        [Test]
        public void GetTreeMultigenicChromosome([Random(-10, 10, 5)] int a, [Random(-10, 10, 5)] int b)
        {
            var parameters = new double[] { a, b };
            // *Q-b/abbbaaba/aQb-bbbaabaa*Q-/b*abbbbaa
            var multigenicNodes = new IGepNode[]
                                      {
                                          new MultiplicationNode(),     // *Q-b/abbbaaba/aQb-bbbaabaa*Q-/b*abbbbaa
                                          new SquareRootNode(),         // Q-b/abbbaaba/aQb-bbbaabaa*Q-/b*abbbbaa
                                          new SubtractionNode(),        // -b/abbbaaba/aQb-bbbaabaa*Q-/b*abbbbaa
                                          new VariableNode(1),          // b/abbbaaba/aQb-bbbaabaa*Q-/b*abbbbaa
                                          new DivisionNode(),           // /abbbaaba/aQb-bbbaabaa*Q-/b*abbbbaa
                                          new VariableNode(0),          // abbbaaba/aQb-bbbaabaa*Q-/b*abbbbaa
                                          new VariableNode(1),          // bbbaaba/aQb-bbbaabaa*Q-/b*abbbbaa
                                          new VariableNode(1),          // bbaaba/aQb-bbbaabaa*Q-/b*abbbbaa
                                          new VariableNode(1),          // baaba/aQb-bbbaabaa*Q-/b*abbbbaa
                                          new VariableNode(0),          // aaba/aQb-bbbaabaa*Q-/b*abbbbaa
                                          new VariableNode(0),          // aba/aQb-bbbaabaa*Q-/b*abbbbaa
                                          new VariableNode(1),          // ba/aQb-bbbaabaa*Q-/b*abbbbaa
                                          new VariableNode(0),          // a/aQb-bbbaabaa*Q-/b*abbbbaa
                                          new DivisionNode(),           // /aQb-bbbaabaa*Q-/b*abbbbaa
                                          new VariableNode(0),          // aQb-bbbaabaa*Q-/b*abbbbaa
                                          new SquareRootNode(),         // Qb-bbbaabaa*Q-/b*abbbbaa
                                          new VariableNode(1),          // b-bbbaabaa*Q-/b*abbbbaa
                                          new SubtractionNode(),        // -bbbaabaa*Q-/b*abbbbaa
                                          new VariableNode(1),          // bbbaabaa*Q-/b*abbbbaa
                                          new VariableNode(1),          // bbaabaa*Q-/b*abbbbaa
                                          new VariableNode(1),          // baabaa*Q-/b*abbbbaa
                                          new VariableNode(0),          // aabaa*Q-/b*abbbbaa
                                          new VariableNode(0),          // abaa*Q-/b*abbbbaa
                                          new VariableNode(1),          // baa*Q-/b*abbbbaa
                                          new VariableNode(0),          // aa*Q-/b*abbbbaa
                                          new VariableNode(0),          // a*Q-/b*abbbbaa
                                          new MultiplicationNode(),     // *Q-/b*abbbbaa
                                          new SquareRootNode(),         // Q-/b*abbbbaa
                                          new SubtractionNode(),        // -/b*abbbbaa
                                          new DivisionNode(),           // /b*abbbbaa
                                          new VariableNode(1),          // b*abbbbaa
                                          new MultiplicationNode(),     // *abbbbaa
                                          new VariableNode(0),          // abbbbaa
                                          new VariableNode(1),          // bbbbaa
                                          new VariableNode(1),          // bbbaa
                                          new VariableNode(1),          // bbaa
                                          new VariableNode(1),          // baa
                                          new VariableNode(0),          // aa
                                          new VariableNode(0),          // a
                                      };
            var chromosome = new Chromosome(6, 3, 2, FunctionSet, multigenicNodes) { LinkingFunction = new AdditionNode() };
            var tree = chromosome.Tree;
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
            var chromosome = new Chromosome(2, 1, 2, FunctionSet, nodes);
            var actual = chromosome.Tree;

            Assert.That(actual.Evaluate(new double[0]), Is.EqualTo(expected.Evaluate(new double[0])).Within(0.0001));
        }
    }
}
