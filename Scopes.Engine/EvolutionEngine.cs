﻿namespace Scopes.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MathNet.Numerics.Random;

    using Scopes.Engine.Crossover;
    using Scopes.Engine.Fitness;
    using Scopes.Engine.Mutation;
    using Scopes.Engine.Selection;
    using Scopes.Engine.Termination;

    public class EvolutionEngine
    {
        private readonly MersenneTwister random = MersenneTwister.Default;
        private readonly double crossoverRate;
        private readonly double mutationRate;
        public ICrossover Crossover { get; set; }
        public IMutation Mutation { get; set; }
        public ISelection Selection { get; set; }

        public EvolutionEngine(double crossoverRate, double mutationRate)
        {
            this.crossoverRate = crossoverRate;
            this.mutationRate = mutationRate;
        }

        public IPopulation Evolve(IPopulation initial, ITerminationCondition terminationCondition, Dictionary<double[], double> dataSet)
        {
            var current = initial;
            var f = new FitnessEvaluator(dataSet);
            while (!terminationCondition.IsSatisfied()) {
                foreach (var c in current) {
                    c.Fitness = f.Calculate(c);
                }
                current = NextGeneration(current);
            }

            return current;
        }

        private IPopulation NextGeneration(IPopulation population)
        {
            var nextGeneration = population.NextGeneration();
            while (nextGeneration.Size < nextGeneration.Limit)
            {
                var pair = Selection.Select(population);

                if (random.NextDouble() < this.crossoverRate) {
                    pair = Crossover.Crossover(pair[0], pair[1]);
                }

                if (random.NextDouble() < this.mutationRate) {
                    pair = new List<Chromosome> { Mutation.Mutate(pair[0]), Mutation.Mutate(pair[1]) };
                }
                nextGeneration.Add(pair[0]);
                if (nextGeneration.Size < nextGeneration.Limit) {
                    nextGeneration.Add(pair[1]);
                }
            }

            return nextGeneration;
        }
    }
}
