namespace Scopes.Engine.Selection
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using MathNet.Numerics.Random;

    public class TournamentSelection : ISelection
    {
        private readonly MersenneTwister random = MersenneTwister.Default;
        private readonly int tournamentSize;

        public TournamentSelection()
            : this(2)
        {
        }

        public TournamentSelection(int tournamentSize)
        {
            Contract.Requires<ArgumentOutOfRangeException>(tournamentSize > 1);
            this.tournamentSize = tournamentSize;
        }

        [Pure]
        public int TournamentSize { get { return this.tournamentSize; } }

        [Pure]
        public IList<Chromosome> Select(IPopulation population)
        {
            return new List<Chromosome> { Tournament(population), Tournament(population) };
        }

        [Pure]
        private Chromosome Tournament(IPopulation population)
        {
            Contract.Requires<ArgumentNullException>(population != null);

            var chromosomes = new List<Chromosome>(population.Chromosomes);
            var tournament = new List<Chromosome>(this.tournamentSize);
            for (var i = 0; i < this.tournamentSize; i++)
            {
                var idx = this.random.Next(chromosomes.Count);
                tournament.Add(chromosomes[idx]);
                // To prevent re-selection.
                chromosomes.RemoveAt(idx);
            }
            var results = tournament.OrderBy(chromosome => chromosome.Fitness).ToArray();
            return results[0];
        }
    }
}
