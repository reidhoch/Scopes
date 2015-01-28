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

        public IPopulation Generate(ISet<Func<IFunctionNode>> functionSet, int size, int headLength, int numGenes)
        {
            Contract.Requires(functionSet != null);
            Contract.Requires(size >= 2);
            Contract.Requires(headLength > 0);
            Contract.Requires(numGenes >= 1);
            Contract.Ensures(Contract.Result<IPopulation>() != null);

            var chromosomes = new List<Chromosome>(size);
            for (var idx = 0; idx < size; idx++) {
                chromosomes.Add(new Chromosome(headLength, numGenes, functionSet));
            }
            return new Population(chromosomes);
        }
    }
}
