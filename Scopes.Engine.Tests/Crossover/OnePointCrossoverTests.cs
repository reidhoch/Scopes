namespace Scopes.Engine.Tests.Crossover
{
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;

    using Scopes.Engine.Crossover;
    using Scopes.Engine.Nodes;

    [TestFixture]
    public class OnePointCrossoverTests
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
        public void Crossover()
        {
            var add = new AdditionNode();
            var subtract = new SubtractionNode();

            var fNodes = new List<IGepNode> { add, subtract, add, subtract, subtract, add, subtract, add, add };
            var mNodes = new List<IGepNode> { subtract, add, add, subtract, add, subtract, add, add, add };
            var father = new Chromosome(4, 1, FunctionSet, fNodes);
            var mother = new Chromosome(4, 1, FunctionSet, mNodes);
            var crossover = new OnePointCrossover();
            for (var i = 0; i < 20; i++) {
                var result = crossover.Crossover(father, mother);

                var son = result[0].Nodes;
                var daughter = result[1].Nodes;
                // These should never change
                Assert.That(fNodes[2], Is.EqualTo(son[2]));
                Assert.That(mNodes[2], Is.EqualTo(daughter[2]));
                Assert.That(fNodes[3], Is.EqualTo(son[3]));
                Assert.That(mNodes[3], Is.EqualTo(daughter[3]));
                Assert.That(fNodes[7], Is.EqualTo(son[7]));
                Assert.That(mNodes[7], Is.EqualTo(daughter[7]));
            }
        }

        [Test]
        public void GetCrossoverPoints()
        {
            var crossover = new OnePointCrossover();
            Assert.That(crossover.CrossoverPoints, Is.EqualTo(1));
        }
    }
}
