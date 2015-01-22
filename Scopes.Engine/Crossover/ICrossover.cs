namespace Scopes.Engine.Crossover
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    [ContractClass(typeof(CrossoverContract))]
    public interface ICrossover
    {
        List<Chromosome> Crossover(Chromosome father, Chromosome mother);
    }

    [ContractClassFor(typeof(ICrossover))]
    internal abstract class CrossoverContract : ICrossover
    {
        public List<Chromosome> Crossover(Chromosome father, Chromosome mother)
        {
            Contract.Requires<ArgumentNullException>(father != null);
            Contract.Requires<ArgumentNullException>(mother != null);
            Contract.Requires(father.Length == mother.Length);
            Contract.Ensures(Contract.Result<List<Chromosome>>() != null);
            Contract.Ensures(Contract.Result<List<Chromosome>>().Count == 2);
            return default(List<Chromosome>);
        }
    }
}
