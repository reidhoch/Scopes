namespace Scopes.Engine.Tests
{
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;

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
                                  { new[] { 6.9408 }, 44.90975 },
                                  { new[] { -7.8664 }, 7.340925 },
                                  { new[] { -2.7861 }, -4.47712 },
                                  { new[] { -5.0944 }, -2.30674 },
                                  { new[] { 9.4895 }, 73.49381 },
                                  { new[] { -9.6197 }, 17.41021 },
                                  { new[] { -9.4145 }, 16.07291 },
                                  { new[] { -0.1432 }, -0.41935 },
                                  { new[] { 0.9107 }, 3.146787 },
                                  { new[] { 2.1762 }, 8.896523 }
                              };
            var engine = new EvolutionEngine
                             {
                                 OnePointCrossoverRate = 0.4,
                                 TwoPointCrossoverRate = 0.2,
                                 MutationRate = .044,
                                 Mutation = new SinglePointMutation(),
                                 TranspositionRate = 0.1,
                                 Selection = new TournamentSelection()
                             };
            var initialPopulation = ChromosomeFactory.Instance.Generate(functionSet, 20, 7, 1, 1);
            (initialPopulation as Population).ElitismRate = 0.05;
            var pop = engine.Evolve(
                initialPopulation,
                new FixedGenerationCountTerminationCondition(500),
                dataSet);
            var best = pop.Chromosomes[0].Tree;
            var answer = best.Evaluate(new[] { 2.1762 });
            Assert.That(answer, Is.EqualTo(8.896523));
        }
    }
}
