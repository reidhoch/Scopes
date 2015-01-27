namespace Scopes.Engine.Tests.Selection
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using NUnit.Framework;

    using Scopes.Engine.Nodes;
    using Scopes.Engine.Selection;

    [TestFixture]
    public class TournamentSelectionTests
    {
        [Test]
        public void DefaultTournamentSize()
        {
            var actual = new TournamentSelection();
            Assert.That(actual.TournamentSize, Is.EqualTo(2));
        }

        [Test]
        public void GetTournamentSize()
        {
            const int Expected = 10;
            var actual = new TournamentSelection(Expected);
            Assert.That(actual.TournamentSize, Is.EqualTo(Expected));
        }

        [Test]
        public void Select()
        {
            var selection = new TournamentSelection();
            var population = new Population { Limit = 10 };
            for (var i = 0; i < 10; i++) {
                population.Add(new Dummy());
            }

            for (var i = 0; i < 10; i++)
            {
                var selected = selection.Select(population);
                var first = selected[0];
                var second = selected[1];
                // The least fit chromosome should never be selected.
                Assert.That(first.Fitness < 9.0d);
                Assert.That(second.Fitness < 9.0d);
            }
        }

        internal class Dummy : Chromosome
        {
            private static readonly ISet<Func<IFunctionNode>> DummyFunctionSet = new HashSet<Func<IFunctionNode>>
                                  {
                                      () => new SquareRootNode(),
                                      () => new AdditionNode(),
                                      () => new SubtractionNode(),
                                      () => new MultiplicationNode(),
                                      () => new DivisionNode()
                                  };
            private static readonly ISet<Func<ITerminalNode>> DummyTerminalSet = new HashSet<Func<ITerminalNode>>
                                  {
                                      () => new ConstantNode(),
                                      () => new VariableNode()
                                  };
            private static int fitness;

            internal Dummy()
                : base(10, 1, DummyFunctionSet, DummyTerminalSet)
            {
                this.Fitness = fitness;
                Interlocked.Increment(ref fitness);
            }
        }
    }
}
