namespace Scopes.Engine.Fitness
{
    using System;
    using System.Diagnostics.Contracts;

    [ContractClass(typeof(FitnessEvaluatorContract))]
    public interface IFitnessEvaluator
    {
        double Calculate(Chromosome chromosome);
    }

    [ContractClassFor(typeof(IFitnessEvaluator))]
    internal abstract class FitnessEvaluatorContract : IFitnessEvaluator
    {
        public double Calculate(Chromosome chromosome)
        {
            Contract.Requires<ArgumentNullException>(chromosome != null);
            return default(double);
        }
    }
}
