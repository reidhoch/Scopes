namespace Scopes.Engine.Selection
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    [ContractClass(typeof(SelectionContract))]
    public interface ISelection
    {
        IList<Chromosome> Select(IPopulation population);
    }

    [ContractClassFor(typeof(ISelection))]
    internal abstract class SelectionContract : ISelection
    {
        public IList<Chromosome> Select(IPopulation population)
        {
            Contract.Requires(population != null);
            Contract.Ensures(Contract.Result<IList<Chromosome>>() != null);
            return default(IList<Chromosome>);
        }
    }
}