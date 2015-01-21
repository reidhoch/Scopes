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
        [Test]
        public void Constructor()
        {
            var functionSet = new HashSet<Func<IFunctionNode>>
                                  {
                                      () => new SquareRootNode(),
                                      () => new AdditionNode(),
                                      () => new SubtractionNode(),
                                      () => new MultiplicationNode(),
                                      () => new DivisionNode()
                                  };
            var terminalSet = new HashSet<Func<ITerminalNode>>
                                  {
                                      () => new ConstantNode(),
                                      () => new VariableNode()
                                  };
            Assert.DoesNotThrow(() => new Chromosome(10, 1, functionSet, terminalSet));
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructorNegativeHeadLength()
        {
            var functionSet = new HashSet<Func<IFunctionNode>>
                                  {
                                      () => new SquareRootNode(),
                                      () => new AdditionNode(),
                                      () => new SubtractionNode(),
                                      () => new MultiplicationNode(),
                                      () => new DivisionNode()
                                  };
            var terminalSet = new HashSet<Func<ITerminalNode>>
                                  {
                                      () => new ConstantNode(),
                                      () => new VariableNode()
                                  };
            new Chromosome(-1, 1, functionSet, terminalSet);
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructorZeroNumGenes()
        {
            var functionSet = new HashSet<Func<IFunctionNode>>
                                  {
                                      () => new SquareRootNode(),
                                      () => new AdditionNode(),
                                      () => new SubtractionNode(),
                                      () => new MultiplicationNode(),
                                      () => new DivisionNode()
                                  };
            var terminalSet = new HashSet<Func<ITerminalNode>>
                                  {
                                      () => new ConstantNode(),
                                      () => new VariableNode()
                                  };
            new Chromosome(10, 0, functionSet, terminalSet);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNullNodes()
        {
            var functionSet = new HashSet<Func<IFunctionNode>>
                                  {
                                      () => new SquareRootNode(),
                                      () => new AdditionNode(),
                                      () => new SubtractionNode(),
                                      () => new MultiplicationNode(),
                                      () => new DivisionNode()
                                  };
            var terminalSet = new HashSet<Func<ITerminalNode>>
                                  {
                                      () => new ConstantNode(),
                                      () => new VariableNode()
                                  };
            new Chromosome(10, 1, functionSet, terminalSet, null);
        }

        [Test]
        public void ConstructorNodes()
        {
            var functionSet = new HashSet<Func<IFunctionNode>>
                                  {
                                      () => new SquareRootNode(),
                                      () => new AdditionNode(),
                                      () => new SubtractionNode(),
                                      () => new MultiplicationNode(),
                                      () => new DivisionNode()
                                  };
            var terminalSet = new HashSet<Func<ITerminalNode>>
                                  {
                                      () => new ConstantNode(),
                                      () => new VariableNode()
                                  };
            var expected = new List<IGepNode> { new ConstantNode() };
            var chromosome = new Chromosome(10, 1, functionSet, terminalSet, expected);
            Assert.That(chromosome.Nodes, Is.EquivalentTo(expected));
        }

        [Test]
        public void GetFunctionSet()
        {
            var functionSet = new HashSet<Func<IFunctionNode>>
                                  {
                                      () => new SquareRootNode(),
                                      () => new AdditionNode(),
                                      () => new SubtractionNode(),
                                      () => new MultiplicationNode(),
                                      () => new DivisionNode()
                                  };
            var terminalSet = new HashSet<Func<ITerminalNode>>
                                  {
                                      () => new ConstantNode(),
                                      () => new VariableNode()
                                  };
            var nodes = new List<IGepNode> { new ConstantNode() };
            var chromosome = new Chromosome(10, 1, functionSet, terminalSet, nodes);
            Assert.That(chromosome.FunctionSet, Is.EquivalentTo(functionSet));
        }

        [Test]
        public void GetTerminalSet()
        {
            var functionSet = new HashSet<Func<IFunctionNode>>
                                  {
                                      () => new SquareRootNode(),
                                      () => new AdditionNode(),
                                      () => new SubtractionNode(),
                                      () => new MultiplicationNode(),
                                      () => new DivisionNode()
                                  };
            var terminalSet = new HashSet<Func<ITerminalNode>>
                                  {
                                      () => new ConstantNode(),
                                      () => new VariableNode()
                                  };
            var nodes = new List<IGepNode> { new ConstantNode() };
            var chromosome = new Chromosome(10, 1, functionSet, terminalSet, nodes);
            Assert.That(chromosome.TerminalSet, Is.EquivalentTo(terminalSet));
        }

        [Test]
        public void GetHeadLength([Random(5, 10, 5)]int headLength)
        {
            var functionSet = new HashSet<Func<IFunctionNode>>
                                  {
                                      () => new SquareRootNode(),
                                      () => new AdditionNode(),
                                      () => new SubtractionNode(),
                                      () => new MultiplicationNode(),
                                      () => new DivisionNode()
                                  };
            var terminalSet = new HashSet<Func<ITerminalNode>>
                                  {
                                      () => new ConstantNode(),
                                      () => new VariableNode()
                                  };
            var nodes = new List<IGepNode> { new ConstantNode() };
            var chromosome = new Chromosome(headLength, 1, functionSet, terminalSet, nodes);
            Assert.That(chromosome.HeadLength, Is.EqualTo(headLength));
        }

        [Test]
        public void GetNumGenes([Random(5, 10, 5)]int numGenes)
        {
            var functionSet = new HashSet<Func<IFunctionNode>>
                                  {
                                      () => new SquareRootNode(),
                                      () => new AdditionNode(),
                                      () => new SubtractionNode(),
                                      () => new MultiplicationNode(),
                                      () => new DivisionNode()
                                  };
            var terminalSet = new HashSet<Func<ITerminalNode>>
                                  {
                                      () => new ConstantNode(),
                                      () => new VariableNode()
                                  };
            var nodes = new List<IGepNode> { new ConstantNode() };
            var chromosome = new Chromosome(10, numGenes, functionSet, terminalSet, nodes);
            Assert.That(chromosome.NumGenes, Is.EqualTo(numGenes));
        }
    }
}
