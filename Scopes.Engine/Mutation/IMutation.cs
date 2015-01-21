namespace Scopes.Engine.Mutation
{
    using System.Diagnostics.Contracts;

    [ContractClass(typeof(MutationContract))]
    public interface IMutation
    {
        Chromosome Mutate(Chromosome original);
    }

    [ContractClassFor(typeof(IMutation))]
    internal abstract class MutationContract : IMutation
    {
        public Chromosome Mutate(Chromosome original)
        {
            Contract.Requires(original != null);
            Contract.Ensures(Contract.Result<Chromosome>() != null);
            return default(Chromosome);
        }
    }
}
