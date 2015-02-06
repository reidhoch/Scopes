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
            var engine = new EvolutionEngine
                             {
                                 CrossoverRate = 0.7,
                                 Crossover = new OnePointCrossover(),
                                 MutationRate = .05,
                                 Mutation = new SinglePointMutation(),
                                 Selection = new TournamentSelection()
                             };
            var initialPopulation = ChromosomeFactory.Instance.Generate(functionSet, 20, 4, 1, 2);
            (initialPopulation as Population).ElitismRate = 0.05;
            var pop = engine.Evolve(
                initialPopulation,
                new ElapsedTimeTerminationCondition(TimeSpan.FromSeconds(5)),
                dataSet);
            var best = pop.Chromosomes[0].Tree;
            var answer = best.Evaluate(new[] { 6.0, 6.0 });
            Assert.That(answer, Is.EqualTo(36.0));
        }
    }
}
