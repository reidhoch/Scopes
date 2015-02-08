namespace Scopes.Engine.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using Scopes.Engine.Nodes;

    public class ChromosomeFactory
    {
        private static readonly Lazy<ChromosomeFactory> Lazy = new Lazy<ChromosomeFactory>(() => new ChromosomeFactory());

        public static ChromosomeFactory Instance { get { return Lazy.Value; } }

        public IPopulation Generate(ISet<Func<IFunctionNode>> functionSet, int size, int headLength, int numGenes, int parameterCount)
        {
            Contract.Requires(functionSet != null);
            Contract.Requires(size >= 2);
            Contract.Requires(headLength > 0);
            Contract.Requires(numGenes >= 1);
            Contract.Requires(parameterCount >= 0);
            Contract.Ensures(Contract.Result<IPopulation>() != null);

            var chromosomes = new List<Chromosome>(size);
            for (var idx = 0; idx < size; idx++) {
                var chromosome = new Chromosome(headLength, numGenes, parameterCount, functionSet);
                chromosome.Generate();
                chromosomes.Add(chromosome);
            }
            return new Population(chromosomes) { Limit = size };
        }
    }
}
