namespace Scopes.Engine.Tests
{
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;

    using Scopes.Engine.Crossover;
    using Scopes.Engine.Factories;
    using Scopes.Engine.Mutation;
    using Scopes.Engine.Nodes;
    using Scopes.Engine.Selection;
    using Scopes.Engine.Termination;

    [TestFixture]
    public class EvolutionEngineTests
    {
        [Test]
        public void Test()
        {
            var functionSet = new HashSet<Func<IFunctionNode>>
                                  {
                                      () => new SquareRootNode(),
                                      () => new AdditionNode(),
                                      () => new SubtractionNode(),
                                      () => new MultiplicationNode(),
                                      () => new DivisionNode()
                                  };
            var dataSet = new Dictionary<double[], double>
                              {
                                  { new[] { 1.0, 1.0 }, 1.0 },
                                  { new[] { 2.0, 2.0 }, 4.0 },
                                  { new[] { 3.0, 3.0 }, 9.0 },
                                  { new[] { 4.0, 4.0 }, 16.0 },
                                  { new[] { 5.0, 5.0 }, 25.0 }
                              };
            var engine = new EvolutionEngine(.7, .05)
                             {
                                 Crossover = new OnePointCrossover(),
                                 Mutation = new SinglePointMutation(),
                                 Selection = new TournamentSelection()
                             };
            var initialPopulation = ChromosomeFactory.Instance.Generate(functionSet, 50, 10, 1);
            var pop = engine.Evolve(
                initialPopulation,
                new ElapsedTimeTerminationCondition(TimeSpan.FromSeconds(30)),
                dataSet);
        }
    }
}
