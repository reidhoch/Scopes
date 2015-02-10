namespace Scopes.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using MathNet.Numerics.Random;

    using Scopes.Engine.Crossover;
    using Scopes.Engine.Fitness;
    using Scopes.Engine.Mutation;
    using Scopes.Engine.Selection;
    using Scopes.Engine.Termination;

    public class EvolutionEngine
    {
        private readonly MersenneTwister random = MersenneTwister.Default;
        private readonly ICrossover onePointCrossover = new OnePointCrossover();
        private readonly ICrossover twoPointCrossover = new TwoPointCrossover();
        private readonly IMutation transposition = new Transposition();
        private readonly IMutation rootTransposition = new RootTransposition();
        private double onePointCrossoverRate;
        private double twoPointCrossoverRate;
        private double transpositionRate;
        private double rootTranspositionRate;
        private double mutationRate;
        public IMutation Mutation { get; set; }
        public ISelection Selection { get; set; }

        public double OnePointCrossoverRate
        {
            get
            {
                return this.onePointCrossoverRate;
            }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value >= 0.0 && value <= 1.0);
                this.onePointCrossoverRate = value;
            }
        }

        public double TwoPointCrossoverRate
        {
            get
            {
                return this.twoPointCrossoverRate;
            }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value >= 0.0 && value <= 1.0);
                this.twoPointCrossoverRate = value;
            }
        }

        public double MutationRate
        {
            get
            {
                return this.mutationRate;
            }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value >= 0.0 && value <= 1.0);
                this.mutationRate = value;
            }
        }

        public double TranspositionRate
        {
            get
            {
                return this.transpositionRate;
            }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value >= 0.0 && value <= 1.0);
                this.transpositionRate = value;
            }
        }

        public double RootTranspositionRate
        {
            get
            {
                return this.rootTranspositionRate;
            }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value >= 0.0 && value <= 1.0);
                this.rootTranspositionRate = value;
            }
        }

        public IPopulation Evolve(IPopulation initial, ITerminationCondition terminationCondition, Dictionary<double[], double> dataSet)
        {
            Contract.Requires<ArgumentNullException>(initial != null);
            Contract.Requires<ArgumentNullException>(terminationCondition != null);
            Contract.Requires<ArgumentNullException>(dataSet != null);
            Contract.Ensures(Contract.Result<IPopulation>() != null);
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
            Contract.Requires<ArgumentNullException>(population != null);
            Contract.Ensures(Contract.Result<IPopulation>() != null);
            var nextGeneration = population.NextGeneration();
            while (nextGeneration.Size < nextGeneration.Limit)
            {
                var pair = Selection.Select(population);

                if (random.NextDouble() < this.OnePointCrossoverRate) {
                    pair = this.onePointCrossover.Crossover(pair[0], pair[1]);
                }
                if (random.NextDouble() < this.TwoPointCrossoverRate) {
                    pair = this.twoPointCrossover.Crossover(pair[0], pair[1]);
                }
                if (random.NextDouble() < this.MutationRate) {
                    pair = new List<Chromosome> { Mutation.Mutate(pair[0]), Mutation.Mutate(pair[1]) };
                }
                if (random.NextDouble() < this.TranspositionRate) {
                    pair = new List<Chromosome> { transposition.Mutate(pair[0]), transposition.Mutate(pair[1]) };
                }
                if (random.NextDouble() < this.RootTranspositionRate) {
                    pair = new List<Chromosome> { rootTransposition.Mutate(pair[0]), rootTransposition.Mutate(pair[1]) };
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

